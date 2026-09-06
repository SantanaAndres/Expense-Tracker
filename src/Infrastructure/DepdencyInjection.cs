using Application.Abstraction.Repository;
using Infrastructure.Persistence.Repositories;
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
        return services;
    }
}