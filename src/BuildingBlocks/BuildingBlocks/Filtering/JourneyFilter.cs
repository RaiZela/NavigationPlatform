namespace BuildingBlocks.Filtering;

public class JourneyFilter
{
    public Guid UserId { get; set; } = Guid.Empty;
    public TransportType TransportType { get; set; } = TransportType.None;
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public DateTime? ArrivalDateFrom { get; set; }
    public DateTime? ArrivalDateTo { get; set; }
    public decimal MinDistance { get; set; }
    public decimal MaxDistance { get; set; }
}
