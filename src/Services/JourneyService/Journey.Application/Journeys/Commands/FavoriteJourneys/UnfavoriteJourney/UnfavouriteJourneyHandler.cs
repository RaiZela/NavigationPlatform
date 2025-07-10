using Journey.Application.Journeys.Helpers;

namespace Journey.Application.Journeys.Commands.FavoriteJourneys.UnfavoriteJourney;

public record UnfavouriteJourneyHandler(IApplicationDbContext dbContext, ICurrentUserService CurrentUserService)
    : ICommandHandler<UnfavouriteJourneyCommand, UnfavouriteJourneyResult>
{
    public async Task<UnfavouriteJourneyResult> Handle(UnfavouriteJourneyCommand command, CancellationToken cancellationToken)
    {
        var userName = CurrentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new UserNotFoundException(userName);

        var journey = await dbContext.Journeys.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Id);

        var favoriteJourney = FavoriteJourneyHelper.CreateAsFavorite(user.Id, command.Id);

        dbContext.FavoriteJourneys.Remove(favoriteJourney);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UnfavouriteJourneyResult(true);
    }

}


