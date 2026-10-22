
using BancoDigital.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BancoDigital.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        int statusCode = exception switch
        {
            BusinessException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        string message = statusCode == 500
            ? "Ocurrió un error interno del servidor."
            : exception.Message;

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                statusCode,
                message
            },
            cancellationToken);

        return true;
    }
}   