using BuildingBlocks.Pagination;

namespace Journey.Application.Journeys.Queries.GetJourneys;

public record GetJourneysQuery(PaginationRequest PaginationRequest)
    : IQuery<GetJourneysResult>;

public record GetJourneysResult(PaginatedResult<JourneyDto> Journeys);
