namespace VetFlow.Domain.Entities;

using VetFlow.Domain.Common;

/// <summary>
/// Transactional outbox message for reliable event publishing.
/// </summary>
public sealed class OutboxMessage : BaseEntity<Guid>
{
    public string Type { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime OccurredOnUtc { get; private set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(Guid id, string type, string content, DateTime occurredOnUtc)
    {
        Id = id;
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void MarkProcessed(DateTime processedOnUtc)
    {
        ProcessedOnUtc = processedOnUtc;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void MarkFailed(string error)
    {
        Error = error;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
