using Application.Abstraction.Repository;
using Application.Feature.User.Add;
using ExpenseTrackerApi.MiddleWare;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(AddUserCommand).Assembly);

    opts.UseFluentValidation();
    
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IFixedCostRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IExpenseRecordRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IExpenseTypeRepository>();
    opts.CodeGeneration.AlwaysUseServiceLocationFor<IUserRepository>();
});

builder.Services.AddWolverineHttp();

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfraestructuraBackend();

var app = builder.Build();

app.UseExceptionHandler();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapWolverineEndpoints(opts =>
{
    opts.UseFluentValidationProblemDetailMiddleware();
});

app.Run();
