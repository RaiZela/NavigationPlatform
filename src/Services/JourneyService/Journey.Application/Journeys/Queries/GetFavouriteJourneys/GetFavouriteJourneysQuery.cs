namespace Journey.Application.Journeys.Queries.GetFavouriteJourneys;

public record GetFavouriteJourneysQuery(4)
    : IQuery<GetFavouriteJourneysResult>;


public record GetFavouriteJourneysResult(IEnumerable<JourneyDto> Journeys);