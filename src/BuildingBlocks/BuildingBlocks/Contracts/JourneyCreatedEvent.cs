namespace BuildingBlocks.Contracts;

public record JourneyCreatedEvent
{
    public Guid Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}


