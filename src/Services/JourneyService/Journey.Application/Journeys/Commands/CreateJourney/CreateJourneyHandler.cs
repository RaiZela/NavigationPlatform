using Journey.Application.Dtos;
using Mapster;

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

    private JourneyEntity CreateNewJourney(JourneyDto journey)
    {
        JourneyEntity newJourney = journey.Adapt<JourneyEntity>();

        newJourney.Id = Guid.NewGuid();

        return newJourney;
    }
}
