using Journey.Domain.Models;
using NotificationService.Enum;

namespace NotificationService.Models;

public class Journey
{
    public Guid Id { get; set; }
    public string StartLocation { get; private set; }
    public DateTime StartTime { get; private set; }
    public string ArrivalLocation { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public int TransportType { get; private set; }
    public decimal DistanceKm { get; private set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedByUser { get; set; }
    public DateTime? LastModified { get; set; }
    public string LastModifiedByUser { get; set; }

}

