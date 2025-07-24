
using Journey.Application.Journeys.Helpers;

namespace Journey.Application.Journeys.Queries.GetFilteredJourneys;

public class GetFilteredJourneysHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetFilteredJourneysQuery, GetFilteredJourneysResult>
{
    public async Task<GetFilteredJourneysResult> Handle(GetFilteredJourneysQuery request, CancellationToken cancellationToken)
    {
        var pageIndex = request.PaginationRequest.PageIndex;
        var pageSize = request.PaginationRequest.PageSize;

        var query = dbContext.Journeys.AsQueryable();

        if (request is not null)
            query = FilterQueryBuilder.ApplyFilter(query, request.Filter);

        var totalCount = await query.LongCountAsync(cancellationToken);

        var journeys = await dbContext.Journeys
       .Skip(pageSize * pageIndex)
       .Take(pageSize)
       .ToListAsync(cancellationToken);

        var config = new TypeAdapterConfig();
        config.NewConfig<JourneyEntity, JourneyDto>()
            .Map(dest => dest.DistanceKm, src => src.DistanceKm.Value);

        return new GetFilteredJourneysResult(
            new PaginatedResult<JourneyDto>(
                pageIndex,
                pageSize,
                totalCount,
                journeys.Adapt<IEnumerable<JourneyDto>>(config)));
    }
}
