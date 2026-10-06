using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ExpenseTrackerWorker;

public class FixedCostSchedule(ISchedulerFactory schedulerFactory, ExpenseTrackerDbContext dbContext)
{
    public async Task ScheduleDatabaseJobsAsync(CancellationToken ct = default)
    {
        IScheduler scheduler = await schedulerFactory.GetScheduler(ct);

        var schedules = await dbContext.FixedCosts.ToListAsync(ct);

        foreach (var config in schedules)
        {
            foreach (var schedule in config.AmountExpenses)
            {
                var triggerKey = new TriggerKey($"trigger_{config.FixedCostId}", "dynamic_triggers");
                var jobKey = new JobKey($"job_{config.FixedCostId}", "dynamic_jobs");
                
                var existingTrigger = await scheduler.GetTrigger(triggerKey, ct) as ICronTrigger;

                if (existingTrigger is not null && existingTrigger.CronExpressionString != schedule.Cron)
                {
                    ITrigger updatedTrigger = TriggerBuilder
                        .Create()
                        .WithIdentity(triggerKey)
                        .WithCronSchedule(schedule.Cron)
                        .ForJob(jobKey).Build();
                    
                    await scheduler.RescheduleJob(triggerKey, updatedTrigger, ct);
                }
                else
                {
                    IJobDetail job = JobBuilder.Create()
                        .WithIdentity($"job_{config.FixedCostId}", "dynamic_jobs")
                        .UsingJobData("FixedCostId", config.FixedCostId) 
                        .UsingJobData("AmountRequestGuid", schedule.ExpenseRecordGuid) 
                        .Build();

                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity($"trigger_{config.FixedCostId}", "dynamic_triggers")
                        .WithCronSchedule(schedule.Cron)
                        .Build();
                
                    await scheduler.ScheduleJob(job, trigger);
                }

            }
        }
    }
}