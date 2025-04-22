namespace Journey.Domain.Models;

public class Journey : Aggregate<Guid>
{
    public string StartLocation { get; private set; }
    public DateTime StartTime { get; private set; }
    public string ArrivalLocation { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public TransportType TransportType { get; private set; }
    public DistanceKM DistanceKm { get; private set; }

    public Journey Create(
        string startLocation,
        DateTime startTime,
        string arrivalLocation,
        DateTime arrivalTime,
        TransportType transportType,
        DistanceKM distanceKM)
    {
        var journey = new Journey
        {
            Id = id,
            StartLocation = startLocation,
            StartTime = startTime,
            ArrivalLocation = arrivalLocation,
            ArrivalTime = arrivalTime,
            TransportType = transportType,
            DistanceKm = DistanceKM.Of(distanceKM.Value)
        };

        journey.AddDomainEvent(new JourneyCreatedEvent(journey));
        return journey;
    }

    public void Update(
      string startLocation,
      DateTime startTime,
      string arrivalLocation,
      DateTime arrivalTime,
      TransportType transportType,
      DistanceKM distanceKM)
    {
        StartLocation = startLocation;
        StartTime = startTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        TransportType = transportType;
        DistanceKm = distanceKM;

        AddDomainEvent(new JourneyUpdatedEvent(this));
    }

    public void Delete(Guid Id)
    {
        AddDomainEvent(new JourneyDeletedEvent(Id));
    }
}
