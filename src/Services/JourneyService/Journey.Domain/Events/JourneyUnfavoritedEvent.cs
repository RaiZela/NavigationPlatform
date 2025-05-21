namespace Journey.Domain.Events;

public record JourneyUnfavoritedEvent(Guid Id) : IDomainEvent;

