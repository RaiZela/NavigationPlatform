namespace Journey.Application.Journeys.Queries.GetJourney;

public record GetJourneysByUserQuery(Guid UserId)
    : IQuery<GetJourneysByUserResult>;


public record GetJourneysByUserResult(IEnumerable<JourneyDto> Journeys);