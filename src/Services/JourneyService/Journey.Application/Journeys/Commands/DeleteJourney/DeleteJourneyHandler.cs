namespace Journey.Application.Journeys.Commands.DeleteJourney;

public class DeleteJourneyHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<DeleteJourneyCommand, DeleteJourneyResult>
{
    public async Task<DeleteJourneyResult> Handle(DeleteJourneyCommand command, CancellationToken cancellationToken)
    {
        var userName = currentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new JourneyNoContentException();

        var journey = await dbContext.Journeys
     .FirstOrDefaultAsync(x => x.Id == command.Id && x.CreatedByUserId == user.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Id);

        journey.Delete();
        dbContext.Journeys.Remove(journey);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteJourneyResult(true);
    }
}