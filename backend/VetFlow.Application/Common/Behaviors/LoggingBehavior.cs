using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace VetFlow.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that logs request/response timing.
/// Warns on slow requests (> 500ms) to catch N+1 and inefficient queries early.
/// </summary>
public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private const int SlowRequestThresholdMs = 500;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogDebug("Handling {RequestName}", requestName);

        var sw = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        sw.Stop();

        if (sw.ElapsedMilliseconds > SlowRequestThresholdMs)
            logger.LogWarning("Slow request detected: {RequestName} took {ElapsedMs}ms",
                requestName, sw.ElapsedMilliseconds);
        else
            logger.LogDebug("Handled {RequestName} in {ElapsedMs}ms",
                requestName, sw.ElapsedMilliseconds);

        return response;
    }
}
