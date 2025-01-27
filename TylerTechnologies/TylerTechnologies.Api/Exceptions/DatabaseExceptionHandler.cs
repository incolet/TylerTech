using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TylerTechnologies.Api.Exceptions;

/// <summary>
/// Handles exceptions that occur during database operations.
/// </summary>
/// <param name="logger"></param>
public class DatabaseExceptionHandler(ILogger<DatabaseExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Attempts to handle the exception.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Returns true if the exception was handled, otherwise false.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not (DbUpdateException or SqlException))
            return false; 

        logger.LogError(exception, "Database error occurred");

        context.Response.StatusCode = exception switch
        {
            DbUpdateException dbEx when (dbEx.InnerException as SqlException)?.Number == 2627 
                => StatusCodes.Status409Conflict,
            DbUpdateException dbEx when (dbEx.InnerException as SqlException)?.Number == 547 
                => StatusCodes.Status400BadRequest,
            SqlException sqlEx => sqlEx.Number switch
            {
                2627 => StatusCodes.Status409Conflict,
                547 => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            },
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new
        {
            Title = "Database Error",
            Status = context.Response.StatusCode,
            Detail = exception.Message
        }, cancellationToken: cancellationToken);

        return true; 
    }
}