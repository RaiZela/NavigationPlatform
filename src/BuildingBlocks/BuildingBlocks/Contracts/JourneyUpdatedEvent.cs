namespace BuildingBlocks.Contracts;

public record JourneyUpdatedEvent
{
    public Guid Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
