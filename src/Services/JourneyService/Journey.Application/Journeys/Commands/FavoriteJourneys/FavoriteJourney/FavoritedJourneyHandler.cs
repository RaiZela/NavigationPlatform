namespace Journey.Application.Journeys.Commands.FavoriteJourneys.AddFavoriteJourney;

public record FavoritedJourneyHandler(IApplicationDbContext dbContext, ICurrentUserService CurrentUserService)
    : ICommandHandler<FavoritedJourneyCommand, AddFavoriteJourneyResult>
{
    public async Task<AddFavoriteJourneyResult> Handle(FavoritedJourneyCommand command, CancellationToken cancellationToken)
    {
        var userName = CurrentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new UserNotFoundException(userName);

        var journey = await dbContext.Journeys.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Id);

        var favoriteJourney = AddAsFavorite(user.Id, command.Id);

        dbContext.FavoriteJourneys.Add(favoriteJourney);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddFavoriteJourneyResult(true);
    }

    private FavoriteJourney AddAsFavorite(Guid userId, Guid journeyId)
    {
        var favoriteJourney = FavoriteJourney.Create(userId, journeyId);
        return favoriteJourney;
    }
}
