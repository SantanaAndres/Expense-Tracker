using Application.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DepdencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddScoped<CommunicationServiceFactory>();
        
        return services;
    }
}