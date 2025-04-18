namespace Journey.API.Endpoints;

//Accept a userID parameter
//Constructs a GetJourneysByUserQuery
//Retrieves and returns matching orders

public record GetJourneysByUserResponse(IEnumerable<JourneyDto> Journey);

public class GetJourneysByUser : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetJourneysByUserQuery(userId));
        })
            .WithName("GetJourneysByUser")
.Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.WithSummary("Get Journeys by User")
.WithDescription("Get Journeys by User");
    }
}
