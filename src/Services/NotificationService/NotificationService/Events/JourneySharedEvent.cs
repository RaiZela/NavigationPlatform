namespace NotificationService.Events;

public sealed class JourneySharedEvent
{
    public Models.Journey Journey { get; set; }
    public Guid EventId => Guid.NewGuid();
    public DateTime OcurredOn => DateTime.Now;
}
