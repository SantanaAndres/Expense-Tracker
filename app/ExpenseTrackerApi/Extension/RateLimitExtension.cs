using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ExpenseTrackerApi.Extension;

public static class RateLimitExtension
{
    public static IServiceCollection AddCustomRateLimit(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("LoginLimit", opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(1);
                opt.QueueLimit = 0;
            });
            
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            
            options.OnRejected = async (context, token) =>
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status429TooManyRequests,
                    Title = "Too Many Requests",
                    Detail = "Too many login attempts. Please wait 1 minute."
                };
                await context.HttpContext.Response.WriteAsJsonAsync(problemDetails, token);
            };
        });
        
        return services;
    }
    
    public static WebApplication UseCustomRateLimit(this WebApplication app)
    {
        app.UseRateLimiter();
        
        return app;
    }
}