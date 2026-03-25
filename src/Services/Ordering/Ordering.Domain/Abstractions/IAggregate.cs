namespace Ordering.Domain.Abstractions;

public interface IAggregate<T> : IEntity<T>, IAggregate
{
}

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvents> DomainEvents { get; }
    IDomainEvents[] ClearDomainEvents();
}
