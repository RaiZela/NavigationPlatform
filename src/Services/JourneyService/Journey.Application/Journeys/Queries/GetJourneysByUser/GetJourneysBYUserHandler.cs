namespace Journey.Application.Journeys.Queries.GetJourneysByUser;

public class GetJourneysByUserHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetJourneysByUserQuery, GetJourneysByUserResult>
{
    public async Task<GetJourneysByUserResult> Handle(GetJourneysByUserQuery query, CancellationToken cancellationToken)
    {
        var journeys = await dbContext.Journeys
            .AsNoTracking()
            .Where(x => x.CreatedByUserId == query.UserId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        var config = new TypeAdapterConfig();
        config.NewConfig<JourneyEntity, JourneyDto>()
            .Map(dest => dest.DistanceKm, src => src.DistanceKm.Value);
        List<JourneyDto> journeyDtos = journeys.Adapt<List<JourneyDto>>(config);

        return new GetJourneysByUserResult(journeyDtos);
    }

}
