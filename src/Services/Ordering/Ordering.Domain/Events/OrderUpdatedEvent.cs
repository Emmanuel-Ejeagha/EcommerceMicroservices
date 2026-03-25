namespace Ordering.Domain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvents;