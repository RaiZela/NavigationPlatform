using BuildingBlocks.Filtering;

namespace Journey.Application.Journeys.Queries.GetFilteredJourneys;

public record GetFilteredJourneysQuery(JourneyFilter Filter, PaginationRequest PaginationRequest)
    : IQuery<GetFilteredJourneysResult>;
public record GetFilteredJourneysResult(PaginatedResult<JourneyDto> Journeys);

