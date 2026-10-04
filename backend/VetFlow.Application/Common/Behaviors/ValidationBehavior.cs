using MediatR;
using Microsoft.Extensions.Logging;
using VetFlow.Shared.Results;

namespace VetFlow.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline behavior that runs FluentValidation for every command/query.
/// Returns a validation failure Result instead of throwing an exception.
/// </summary>
public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<FluentValidation.IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next(cancellationToken);

        var context = new FluentValidation.ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next(cancellationToken);

        logger.LogWarning("Validation failed for {RequestType}: {Errors}",
            typeof(TRequest).Name,
            string.Join("; ", failures.Select(f => f.ErrorMessage)));

        // Return as Result<T> failure when the response type supports it
        var firstError = Error.Validation(failures[0].PropertyName, failures[0].ErrorMessage);

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var failureMethod = typeof(Result<>)
                .MakeGenericType(typeof(TResponse).GetGenericArguments())
                .GetMethod(nameof(Result<object>.Failure))!;
            return (TResponse)failureMethod.Invoke(null, [firstError])!;
        }

        throw new FluentValidation.ValidationException(failures);
    }
}
