
using Journey.Application.Journeys.Helpers;

namespace Journey.Application.Journeys.Commands.ShareJourney;

public record SharedJourneyHandler(IApplicationDbContext dbContext, ICurrentUserService CurrentUserService)
    : ICommandHandler<SharedJourneyCommand, SharedJourneyResult>
{
    public async Task<SharedJourneyResult> Handle(SharedJourneyCommand command, CancellationToken cancellationToken)
    {
        var userName = CurrentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new UserNotFoundException(userName);

        var journey = await dbContext.Journeys.FirstOrDefaultAsync(x => x.Id == command.JourneyId, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.JourneyId);

        var sharedJourney = SharedJourneyHelper.CreateAsFavorite(user.Id, command.JourneyId, command.UsersIds);
    }
}
