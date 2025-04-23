using Microsoft.AspNetCore.Authorization;

namespace Journey.Application.Journeys.Commands.CreateJourney;

public class CreateJourneyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateJourneyCommand, CreateJourneyResult>
{
    [Authorize]
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

        JourneyEntity newJourney = new JourneyEntity();
        newJourney = newJourney.Create(
         journeyDto.StartLocation,
         journeyDto.StartTime,
             journeyDto.ArrivalLocation,
             journeyDto.ArrivalTime,
             journeyDto.TransportType,
             journeyDto.DistanceKm);

        newJourney.Id = Guid.NewGuid();

        return newJourney;
    }
}
