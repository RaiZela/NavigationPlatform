namespace BuildingBlocks.IntegrationEvents.Abstractions;

public abstract class EventData
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
}
