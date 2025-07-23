namespace Journey.Domain.Models.Journey;

public class Journey : Aggregate<Guid>
{
    private Journey()
    { }
    public string StartLocation { get; private set; }
    public DateTime StartTime { get; private set; }
    public string ArrivalLocation { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public TransportType TransportType { get; private set; }
    public DistanceKM DistanceKm { get; private set; }

    public static Journey Create(
        string startLocation,
        DateTime startTime,
        string arrivalLocation,
        DateTime arrivalTime,
        TransportType transportType,
        DistanceKM distanceKM)
    {

        if (arrivalTime <= startTime)
            throw new DomainException("Arrival time must be after start time.");

        if (distanceKM.Value <= 0)
            throw new DomainException("Distance must be greater than zero.");

        var journey = new Journey
        {
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
      DistanceKM distanceKM,
      Guid createdByUserId)
    {
        StartLocation = startLocation;
        StartTime = startTime;
        ArrivalLocation = arrivalLocation;
        ArrivalTime = arrivalTime;
        TransportType = transportType;
        DistanceKm = distanceKM;
        CreatedByUserId = createdByUserId;

        AddDomainEvent(new JourneyUpdatedEvent(this));
    }

    public void Delete()
    {
        AddDomainEvent(new JourneyDeletedEvent(Id));
    }

}
