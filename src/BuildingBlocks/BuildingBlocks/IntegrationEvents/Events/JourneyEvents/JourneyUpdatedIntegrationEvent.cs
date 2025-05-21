namespace BuildingBlocks.IntegrationEvents.Events.JourneyEvents;

public class JourneyUpdatedIntegrationEvent : EventData
{
    public JourneyEventModel Journey { get; set; }
};
