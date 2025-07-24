using Journey.Domain.Models.Journey;

namespace Journey.Application.Journeys.Helpers;

public static class SharedJourneyHelper
{
    public static List<SharedJourney> CreateAsShared(List<Guid> userIds, Guid journeyId)
    {
        if (userIds == null || userIds.Count == 0)
        {
            throw new ArgumentException("User ID cannot be empty.", nameof(userIds));
        }
        if (journeyId == Guid.Empty)
        {
            throw new ArgumentException("Journey ID cannot be empty.", nameof(journeyId));
        }

        var shared = new List<SharedJourney>();
        foreach (var userId in userIds)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty.", nameof(userIds));
            }
            var sharedJourney = SharedJourney.Create(userId, journeyId);
            shared.Add(sharedJourney);
        }
        return shared;
    }
}
