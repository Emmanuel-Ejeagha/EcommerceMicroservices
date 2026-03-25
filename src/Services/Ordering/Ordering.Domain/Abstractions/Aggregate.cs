namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
    private readonly List<IDomainEvents> _domainEvents = new();  
    public IReadOnlyList<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvents domainEvents)
    {
        _domainEvents.Add(domainEvents);
    }

    public IDomainEvents[] ClearDomainEvents()
    {
        IDomainEvents[] dequeuedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dequeuedEvents;
    }
}