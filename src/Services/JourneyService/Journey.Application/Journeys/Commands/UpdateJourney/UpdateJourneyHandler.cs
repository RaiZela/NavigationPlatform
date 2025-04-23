using Journey.Application.Exceptionsl;

namespace Journey.Application.Journeys.Commands.UpdateJourney;

public class UpdateJourneyHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    : ICommandHandler<UpdateJourneyCommand, UpdateJourneyResult>
{
    public async Task<UpdateJourneyResult> Handle(UpdateJourneyCommand command, CancellationToken cancellationToken)
    {
        var userName = currentUserService.Username;
        var user = dbContext.Users.FirstOrDefault(x => x.Username == userName);

        if (user is null)
            throw new JourneyNoContentException();

        var journey = await dbContext.Journeys
        .FirstOrDefaultAsync(x => x.Id == command.Journey.Id && x.CreatedByUserId == user.Id, cancellationToken);

        if (journey is null)
            throw new JourneyNotFoundException(command.Journey.Id);

        UpdateJourneyWithNewValues(journey, command.Journey);

        dbContext.Journeys.Update(journey);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateJourneyResult(true);
    }


    public void UpdateJourneyWithNewValues(JourneyEntity journey, JourneyDto journeyDto)
    {
        journey.Update(journeyDto.StartLocation,
            journeyDto.StartTime,
            journeyDto.ArrivalLocation,
            journeyDto.ArrivalTime,
            journeyDto.TransportType,
            journeyDto.DistanceKm);
    }
}

