using Application.Abstraction.Repository;
using Application.Abstraction.Services;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepdencyInjection
{
    public static IServiceCollection AddInfraestructuraBackend(this IServiceCollection services)
    {
        services.AddScoped<IExpenseRecordRepository, ExpenseRecordRepository>();
        services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
        services.AddScoped<IFixedCostRepository, FixedCostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunicationService, EmailCommunicationService>();
        services.AddScoped<ICommunicationService, SmsCommunicationService>();
        return services;
    }
}