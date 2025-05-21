namespace Journey.Domain.Events;

public record JourneyUpdatedEvent(Models.Journey.Journey journey) : IDomainEvent, IOutboxCapableEvent;