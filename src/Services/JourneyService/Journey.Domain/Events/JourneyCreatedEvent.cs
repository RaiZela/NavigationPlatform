namespace Journey.Domain.Events;

public record JourneyCreatedEvent(Models.Journey.Journey journey) : IDomainEvent, IOutboxCapableEvent;