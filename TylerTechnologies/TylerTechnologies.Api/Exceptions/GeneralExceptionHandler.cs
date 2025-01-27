using Microsoft.AspNetCore.Diagnostics;

namespace TylerTechnologies.Api.Exceptions;

/// <summary>
/// This class handles general exceptions
/// </summary>
/// <param name="logger"></param>
public class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// This method handles general exceptions
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// returns true if the exception is handled
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            Title = "Internal Server Error",
            Status = context.Response.StatusCode,
            Detail = "An unexpected error occurred. Please try again later."
        }, cancellationToken: cancellationToken);

        return true; 
    }
}