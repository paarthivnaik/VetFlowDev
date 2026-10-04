namespace VetFlow.Shared.Results;

/// <summary>
/// Discriminated union representing either a successful value or an error.
/// Used across all CQRS handlers to avoid exception-driven control flow.
/// </summary>
public sealed class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result(T value) => Value = value;
    private Result(Error error) => Error = error;

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);

    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<Error, TOut> onFailure)
        => IsSuccess ? onSuccess(Value!) : onFailure(Error!);
}

/// <summary>Non-generic result for commands that return no value.</summary>
public sealed class Result
{
    public Error? Error { get; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    private Result() { }
    private Result(Error error) => Error = error;

    public static readonly Result Ok = new();
    public static Result Failure(Error error) => new(error);

    public static implicit operator Result(Error error) => Failure(error);
}
