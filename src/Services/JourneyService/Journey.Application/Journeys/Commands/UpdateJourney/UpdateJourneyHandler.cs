namespace Journey.Application.Journeys.Commands.UpdateJourney;

public class UpdateJourneyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateJourneyCommand, UpdateJourneyResult>
{
    public async Task<UpdateJourneyResult> Handle(UpdateJourneyCommand command, CancellationToken cancellationToken)
    {

        var journey = await dbContext.Journeys
        .FindAsync(command.Journey.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Journey.Id);

        UpdateJourneyWithNewValues(journey, command.Journey);

        dbContext.Journeys.Update(journey);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateJourneyResult(true);
    }


    public void UpdateJourneyWithNewValues(JourneyEntity journey, JourneyDto journeyDto)
    {
        journey.Update(journeyDto.StartLocation,
            journeyDto.StartTime,
            journeyDto.ArrivalLocation,
            journeyDto.ArrivalTime,
            journeyDto.TransportType,
            journeyDto.DistanceKm);
    }
}

