
using Journey.Application.Journeys.Commands.UpdateJourney;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Journey.Application.Journeys.Commands.DeleteJourney;

public class DeleteJourneyHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteJourneyCommand, DeleteJourneyResult>
{
    public async Task<DeleteJourneyResult> Handle(DeleteJourneyCommand command, CancellationToken cancellationToken)
    {
        var journey = await dbContext.Journeys
     .FindAsync(command.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Id);

        dbContext.Journeys.Remove(journey);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteJourneyResult(true);
    }
}