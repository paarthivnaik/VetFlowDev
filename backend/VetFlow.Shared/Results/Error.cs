namespace VetFlow.Shared.Results;

/// <summary>
/// Represents a typed, structured error — never a raw string.
/// </summary>
public sealed record Error(string Code, string Description, ErrorType Type = ErrorType.Failure)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    // ── Common domain errors ────────────────────────────────────────────────
    public static Error NotFound(string entity, object id)
        => new($"{entity}.NotFound", $"{entity} with id '{id}' was not found.", ErrorType.NotFound);

    public static Error Validation(string field, string message)
        => new($"Validation.{field}", message, ErrorType.Validation);

    public static Error Unauthorized(string reason)
        => new("Auth.Unauthorized", reason, ErrorType.Unauthorized);

    public static Error Conflict(string description)
        => new("Conflict", description, ErrorType.Conflict);

    public static Error Unexpected(string description)
        => new("Unexpected", description, ErrorType.Failure);
}

public enum ErrorType
{
    None,
    Failure,
    Validation,
    NotFound,
    Unauthorized,
    Conflict
}
