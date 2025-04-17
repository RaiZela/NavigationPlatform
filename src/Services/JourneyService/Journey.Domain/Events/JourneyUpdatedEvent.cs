namespace Journey.Domain.Events;


public record JourneyUpdatedEvent(Models.Journey journey) : IDomainEvent;