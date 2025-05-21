namespace BuildingBlocks.IntegrationEvents.Events.SharedJourneyEvents;

public sealed class JourneySharedIntegrationEvent : EventData
{
    public JourneyEventModel Journey { get; set; }
}
