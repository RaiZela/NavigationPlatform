namespace Journey.Domain.EventModels;

public class JourneyEventModel
{
    public Guid Id { get; set; }
    public string StartLocation { get; set; }
    public DateTime StartTime { get; set; }
    public string ArrivalLocation { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int TransportType { get; set; }
    public decimal DistanceKm { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedByUser { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedByUser { get; set; }
}
