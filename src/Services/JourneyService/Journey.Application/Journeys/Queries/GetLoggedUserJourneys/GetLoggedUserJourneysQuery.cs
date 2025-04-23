namespace Journey.Application.Journeys.Queries.GetLoggedUserJourneys;

public record GetLoggedUserJourneysQuery()
    : IQuery<GetLoggedUserJourneysResult>;


public record GetLoggedUserJourneysResult(IEnumerable<JourneyDto> Journeys);