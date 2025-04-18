namespace Journey.Application.Journeys.Queries.GetJourneysByUser;

public class GetJourneysByUserHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetJourneysByUserQuery, GetJourneysByUserResult>
{
    public async Task<GetJourneysByUserResult> Handle(GetJourneysByUserQuery query, CancellationToken cancellationToken)
    {
        var journeys = await dbContext.Journeys
            .AsNoTracking()
            .Where(x => x.CreatedByUserId == query.UserId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);


        var journeyDtos = ProjectToJourneyDto(journeys);

        return new GetJourneysByUserResult(journeyDtos);
    }


    private List<JourneyDto> ProjectToJourneyDto(List<JourneyEntity> journeys)
    {
        List<JourneyDto> destinationList = journeys.Adapt<List<JourneyDto>>();
        return destinationList;
    }
}
