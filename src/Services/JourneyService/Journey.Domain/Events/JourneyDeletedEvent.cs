namespace Journey.Domain.Events;

public record JourneyDeletedEvent(Guid Id) : IDomainEvent;