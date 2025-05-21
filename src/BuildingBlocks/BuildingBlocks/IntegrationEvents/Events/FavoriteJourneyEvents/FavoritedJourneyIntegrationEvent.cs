namespace BuildingBlocks.IntegrationEvents.Events.FavoriteJourneyEvents;

public class FavoritedJourneyIntegrationEvent : EventData
{
    public FavoriteJourneyEventModel Journey { get; set; }

}
