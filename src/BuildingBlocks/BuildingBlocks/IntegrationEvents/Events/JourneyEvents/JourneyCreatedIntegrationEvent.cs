namespace BuildingBlocks.IntegrationEvents.Events.JourneyEvents;

public class JourneyCreatedIntegrationEvent : EventData
{
    public JourneyEventModel Journey { get; set; }
};