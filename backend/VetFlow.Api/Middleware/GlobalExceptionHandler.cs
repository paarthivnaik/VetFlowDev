using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VetFlow.Api.Middleware;

namespace VetFlow.Api.Middleware;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    IHostEnvironment environment,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var correlationId = httpContext.Items[CorrelationIdMiddleware.CorrelationIdHeaderName]?.ToString()
            ?? httpContext.TraceIdentifier;

        logger.LogError(exception, "Unhandled exception occurred while processing request. CorrelationId: {CorrelationId}", correlationId);

        var (statusCode, title, detail, extensions) = MapException(exception, environment.IsDevelopment());

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["correlationId"] = correlationId;

        if (extensions is not null)
        {
            foreach (var (key, value) in extensions)
            {
                problemDetails.Extensions[key] = value;
            }
        }

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });
    }

    private static (int StatusCode, string Title, string Detail, Dictionary<string, object?>? Extensions) MapException(
        Exception exception,
        bool isDevelopment)
    {
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return (
                StatusCodes.Status400BadRequest,
                "Validation Failure",
                "One or more validation errors occurred.",
                new Dictionary<string, object?> { ["errors"] = errors }
            );
        }

        var detail = isDevelopment
            ? exception.ToString()
            : "An unexpected error occurred. Please refer to the correlation ID when contacting support.";

        return (
            StatusCodes.Status500InternalServerError,
            "Internal Server Error",
            detail,
            null
        );
    }
}
