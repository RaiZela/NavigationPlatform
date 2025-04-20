namespace Journey.API.Endpoints;
public record GetJourneysByUserResponse(IEnumerable<JourneyDto> Journey);

public class GetJourneysByUser : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/journeys-by-user/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetJourneysByUserQuery(userId));
            return Results.Ok(result);
        })
            .WithName("GetJourneysByUser")
.Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.WithSummary("Get Journeys by User")
.WithDescription("Get Journeys by User");
    }
}
