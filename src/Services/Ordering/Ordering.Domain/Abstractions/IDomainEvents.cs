namespace Ordering.Domain.Abstractions;

public interface IDomainEvents
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
