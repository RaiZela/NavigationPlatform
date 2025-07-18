namespace Journey.Application.Journeys.Queries.GetJourneys;

public class GetJourneysHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetJourneysQuery, GetJourneysResult>
{
    public async Task<GetJourneysResult> Handle(GetJourneysQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Journeys.LongCountAsync(cancellationToken);

        var journeys = await dbContext.Journeys
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var config = new TypeAdapterConfig();
        config.NewConfig<JourneyEntity, JourneyDto>()
            .Map(dest => dest.DistanceKm, src => src.DistanceKm.Value);

        return new GetJourneysResult(
            new PaginatedResult<JourneyDto>(
                pageIndex,
                pageSize,
                totalCount,
                journeys.Adapt<IEnumerable<JourneyDto>>(config)));
    }
}
