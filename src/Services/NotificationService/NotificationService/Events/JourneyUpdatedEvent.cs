namespace NotificationService.Events
{
    public class JourneyUpdatedEvent
    {
        public Models.Journey Journey { get; set; }
        public Guid EventId => Guid.NewGuid();
        public DateTime OcurredOn => DateTime.Now;
    };
}
