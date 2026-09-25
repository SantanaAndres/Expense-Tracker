namespace ExpenseTrackerWorker;

public class FixedCostWorker(ILogger<FixedCostWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("FixedCostWorker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}