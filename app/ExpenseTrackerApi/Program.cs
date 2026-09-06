using Application.Feature.User.Add;
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

builder.Host.UseWolverine(opts =>
{
    opts.Discovery.IncludeAssembly(typeof(AddUserCommand).Assembly);

    opts.UseFluentValidation();
});

builder.Services.AddWolverineHttp();

builder.Services.AddDbContext<ExpenseTrackerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfraestructuraBackend();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapWolverineEndpoints(opts =>
{
    opts.UseFluentValidationProblemDetailMiddleware();
});

app.Run();
