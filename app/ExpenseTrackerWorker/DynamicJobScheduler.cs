using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ExpenseTrackerWorker;

public class DynamicJobScheduler(ISchedulerFactory schedulerFactory, ExpenseTrackerDbContext dbContext)
{
    public async Task ScheaduleDatabaseJobsAsync(CancellationToken cancellationToken)
    {
        IScheduler scheduler = await schedulerFactory.GetScheduler(cancellationToken);
        
        var fixedCosts = await dbContext.FixedCosts.ToListAsync(cancellationToken);

        foreach (var cost in fixedCosts)
        {
            IJobDetail job = JobBuilder.Create()
                .WithIdentity(cost.FixedCostId.ToString())
                .UsingJobData("FixedCostId", cost.FixedCostId)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(cost.FixedCostId.ToString())
                .WithCronSchedule(cost.Cron)
                .Build();
            
            await scheduler.ScheduleJob(job, trigger, cancellationToken: cancellationToken);
        }
        
    }
}