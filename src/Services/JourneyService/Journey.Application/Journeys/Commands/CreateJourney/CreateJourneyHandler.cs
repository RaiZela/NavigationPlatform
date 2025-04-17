using Journey.Application.Dtos;

namespace Journey.Application.Journeys.Commands.CreateJourney;

public class CreateJourneyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateJourneyCommand, CreateJourneyResult>
{
    public async Task<CreateJourneyResult> Handle(CreateJourneyCommand command, CancellationToken cancellationToken)
    {
        var journey = CreateNewJourney(command.Journey);

        dbContext.Journeys.Add(journey);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateJourneyResult(journey.Id);
    }

    private JourneyEntity CreateNewJourney(JourneyDto journey)
    {
        var newJourney = JourneyEntity.Create(
            Guid.NewGuid(),
            journey.StartLocation,
            journey.StartTime,
            journey.ArrivalLocation,
            journey.ArrivalTime,
            journey.TransportType,
            journey.DistanceKm);

        return newJourney;
    }
}
