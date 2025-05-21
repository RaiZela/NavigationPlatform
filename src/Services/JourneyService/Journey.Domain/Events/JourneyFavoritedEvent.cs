namespace Journey.Domain.Events;

public record JourneyFavoritedEvent(FavoriteJourney journey) : IDomainEvent;
