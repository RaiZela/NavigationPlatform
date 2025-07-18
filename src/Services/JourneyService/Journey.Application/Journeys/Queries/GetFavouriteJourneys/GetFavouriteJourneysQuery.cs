namespace Journey.Application.Journeys.Queries.GetFavouriteJourneys;

public record GetFavouriteJourneysQuery(Guid userId)
    : IQuery<GetFavouriteJourneysResult>;


public record GetFavouriteJourneysResult(IEnumerable<JourneyDto> Journeys);