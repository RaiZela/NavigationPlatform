namespace Journey.Domain.Models;

public class Journey : Aggregate<Guid>
{
    public string StartLocation { get; set; }
    public DateTime StartTime { get; set; }
    public string ArrivalLocation { get; set; }
    public DateTime ArrivalTime { get; set; }
    public TransportType TransportType { get; set; }
    public DistanceKM DistanceKm { get; set; }
}
