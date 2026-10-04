namespace VetFlow.Domain.Common;

/// <summary>
/// Base class for all domain entities with strong typed identifiers.
/// </summary>
public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; } = default!;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
