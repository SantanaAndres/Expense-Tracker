using Application.Helper.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.MiddleWare;

public class GlobalExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        switch (exception)
        {
             case NotFoundException notFound:
                await HandleNotFoundAsync(context, notFound, cancellationToken);
                return true;
            
            case InvalidCredentialException invalidCredential:
                await HandleInvalidCredentialsAsync(context, invalidCredential, cancellationToken);
                return true;
            
            default:
                return false;
        }
    }
    
    private static async Task HandleNotFoundAsync(HttpContext context, NotFoundException excepction, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource not found",
            Detail = excepction.Message
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
    
    private static async Task HandleInvalidCredentialsAsync(HttpContext context, InvalidCredentialException exception, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Authentication Failed",
            Detail = exception.Message
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }

    private static async Task HandleUnauthorizedAccessAsync(HttpContext context, UnauthorizedAccessException exception,
        CancellationToken cancellationToken)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized Access",
            Detail = exception.Message
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);    }

}