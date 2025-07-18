namespace Journey.Application.Journeys.Queries.GetLoggedUserJourneys;

public class GetLoggedUserJourneysHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : IQueryHandler<GetLoggedUserJourneysQuery, GetLoggedUserJourneysResult>
{

    public async Task<GetLoggedUserJourneysResult> Handle(GetLoggedUserJourneysQuery request, CancellationToken cancellationToken)
    {
        var userName = currentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new JourneyNoContentException();

        var journeys = await dbContext.Journeys
            .AsNoTracking()
            .Where(x => x.CreatedByUserId == user.Id)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        var config = new TypeAdapterConfig();
        config.NewConfig<JourneyEntity, JourneyDto>()
            .Map(dest => dest.DistanceKm, src => src.DistanceKm.Value);

        List<JourneyDto> journeyDtos = journeys.Adapt<List<JourneyDto>>(config);

        return new GetLoggedUserJourneysResult(journeyDtos);
    }
}
