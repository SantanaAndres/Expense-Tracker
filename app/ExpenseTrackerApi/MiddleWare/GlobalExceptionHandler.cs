using Application.Helper.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApi.MiddleWare;

public class GlobalExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFound)
            return false;
        
        context.Response.StatusCode = StatusCodes.Status404NotFound;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Resource not found",
            Detail = notFound.Message
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}