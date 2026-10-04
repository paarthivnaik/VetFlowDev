namespace VetFlow.Domain.Common;

/// <summary>
/// Aggregate root base class that encapsulates domain events.
/// </summary>
public abstract class AggregateRoot<TId> : BaseEntity<TId>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
