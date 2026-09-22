using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTrackerApi.Extension;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
            };
        });
        
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        
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
    
    public static WebApplication UseCustomAuthentication(this WebApplication app)
    {
        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();
        
        return app;
    }
}