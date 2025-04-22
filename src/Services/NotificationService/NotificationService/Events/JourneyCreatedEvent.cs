namespace NotificationService.Events;

public record JourneyCreatedEvent
{
    public Models.Journey Journey { get; set; }
    public Guid EventId => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
};