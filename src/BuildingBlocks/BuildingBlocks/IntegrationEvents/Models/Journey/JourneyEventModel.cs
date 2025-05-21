namespace BuildingBlocks.IntegrationEvents.Models.Journey;

public class JourneyEventModel
{
    public string StartLocation { get; set; }
    public DateTime StartTime { get; set; }
    public string ArrivalLocation { get; set; }
    public DateTime ArrivalTime { get; set; }
    public TransportType TransportType { get; set; }
    public decimal DistanceKm { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedByUser { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedByUser { get; set; }

}
