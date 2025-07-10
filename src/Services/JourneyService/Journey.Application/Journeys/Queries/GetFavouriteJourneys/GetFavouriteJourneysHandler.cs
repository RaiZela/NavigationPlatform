namespace Journey.Application.Journeys.Queries.GetFavouriteJourneys;

public class GetFavouriteJourneysHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : IQueryHandler<GetFavouriteJourneysQuery, GetFavouriteJourneysResult>
{
    public async Task<GetFavouriteJourneysResult> Handle(GetFavouriteJourneysQuery request, CancellationToken cancellationToken)
    {
        var userName = currentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new JourneyNoContentException();

        var favouriteJourneys = await dbContext.FavoriteJourneys
             .AsNoTracking()
             .Where(x => x.UserId == user.Id)
             .Select(x => x.Journey)
             .OrderBy(x => x.CreatedAt)
             .ToListAsync(cancellationToken);

        List<JourneyDto> journeyDtos = favouriteJourneys.Adapt<List<JourneyDto>>();

        return new GetFavouriteJourneysResult(journeyDtos);
    }
}
