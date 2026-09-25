using ExpenseTrackerWorker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<FixedCostWorker>();

var host = builder.Build();
host.Run();