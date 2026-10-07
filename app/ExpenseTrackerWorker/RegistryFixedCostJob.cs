using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ExpenseTrackerWorker;

[DisallowConcurrentExecution]
public class RegistryFixedCostJob(ExpenseTrackerDbContext dbContext, ILogger<RegistryFixedCostJob> logger) : IJob
{
    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Running RegistryFixedCostJob");

        var scheduler = context.Scheduler;
        var fixedCosts = await dbContext.FixedCosts.ToListAsync(cancellationToken);

        foreach (var fixedCost in fixedCosts)
        {
            foreach (var schedule in fixedCost.AmountExpenses)
            {
                if (string.IsNullOrWhiteSpace(schedule.Cron))
                {
                    logger.LogWarning("Skipping amount expense {ExpenseRecordGuid} of fixed cost {FixedCostId}: Cron is not configured", schedule.ExpenseRecordGuid, fixedCost.FixedCostId);
                    continue;
                }

                var jobKey = new JobKey($"job_{schedule.ExpenseRecordGuid}", "dynamic_jobs");
                var triggerKey = new TriggerKey($"trigger_{schedule.ExpenseRecordGuid}", "dynamic_triggers");

                var existingTrigger = await scheduler.GetTrigger(triggerKey, cancellationToken) as ICronTrigger;

                if (existingTrigger is not null)
                {
                    if (existingTrigger.CronExpressionString != schedule.Cron)
                    {
                        logger.LogInformation("Rescheduling trigger {TriggerKey} to cron {Cron}", triggerKey, schedule.Cron);

                        var updatedTrigger = TriggerBuilder.Create()
                            .WithIdentity(triggerKey)
                            .WithCronSchedule(schedule.Cron, cron => cron
                                .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("America/Panama"))
                                .WithMisfireInstruction(CronTriggerMisfireInstruction.FireAndProceed))
                            .ForJob(jobKey)
                            .Build();

                        await scheduler.RescheduleJob(triggerKey, updatedTrigger, cancellationToken);
                    }

                    continue;
                }

                logger.LogInformation("Scheduling FixedCostJob for fixed cost {FixedCostId} with cron {Cron}", fixedCost.FixedCostId, schedule.Cron);

                var job = JobBuilder.Create<FixedCostJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData("FixedCostId", fixedCost.FixedCostId)
                    .UsingJobData("AmountRequestGuid", schedule.ExpenseRecordGuid.ToString())
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey)
                    .WithCronSchedule(schedule.Cron, cron => cron
                        .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("America/Panama"))
                        .WithMisfireInstruction(CronTriggerMisfireInstruction.FireAndProceed))
                    .ForJob(jobKey)
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
