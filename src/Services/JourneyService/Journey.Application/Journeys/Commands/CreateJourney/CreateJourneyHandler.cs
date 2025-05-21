using Journey.Domain.ValueObjects;

namespace Journey.Application.Journeys.Commands.CreateJourney;

public class CreateJourneyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateJourneyCommand, CreateJourneyResult>
{
    public async Task<CreateJourneyResult> Handle(CreateJourneyCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var journey = CreateNewJourney(command.Journey);

            dbContext.Journeys.Add(journey);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateJourneyResult(journey.Id);

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private JourneyEntity CreateNewJourney(JourneyDto journeyDto)
    {

        var newJourney = JourneyEntity.Create(
         journeyDto.StartLocation,
         journeyDto.StartTime,
         journeyDto.ArrivalLocation,
         journeyDto.ArrivalTime,
         (Domain.Enums.TransportType)journeyDto.TransportType,
        DistanceKM.Of(journeyDto.DistanceKm));

        newJourney.Id = Guid.NewGuid();

        return newJourney;
    }
}
