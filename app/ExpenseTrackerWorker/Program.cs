using Application.Abstraction.Repository;
using Application.Feature.ExpenseRecord.Add;
using ExpenseTrackerWorker;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Wolverine;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfraestructuraBackend();
builder.Services.AddTransient<FixedCostJob>();

builder.Services.AddWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(AddExpenseRecordCommand).Assembly);

    opts.CodeGeneration.AlwaysUseServiceLocationFor<IFixedCostRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IExpenseRecordRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IExpenseTypeRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IUserRepository>();
});

builder.Services.AddQuartz(q =>
{
    var registryJobKey = new JobKey("RegistryFixedCostJob", "system_jobs");

    q.AddJob<RegistryFixedCostJob>(cfg => cfg.WithIdentity(registryJobKey));

    q.AddTrigger(opts => opts
        .ForJob(registryJobKey)
        .WithIdentity("RegistryFixedCostTrigger", "system_triggers")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithInterval(TimeSpan.FromMinutes(15))
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var host = builder.Build();
host.Run();
