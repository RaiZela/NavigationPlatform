namespace Journey.Domain.Events;

public record JourneyCreatedEvent(Models.Journey journey) : IDomainEvent;