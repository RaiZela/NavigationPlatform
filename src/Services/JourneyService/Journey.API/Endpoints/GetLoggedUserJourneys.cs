
namespace Journey.API.Endpoints;

public record GetLoggedUserJourneysResponse(IEnumerable<JourneyDto> Journeys);
public class GetLoggedUserJourneys : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/journeys-by-logged-user", async (ISender sender) =>
        {
            var result = await sender.Send(new GetLoggedUserJourneysQuery());
            var test = result.Adapt<GetLoggedUserJourneysResponse>();
            return Results.Ok(test);
        })
        .WithName("GetJourneysByLoggedUser")
        .Produces<IEnumerable<GetJourneysByUserResponse>>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Journeys by logged ser")
        .WithDescription("Get Journeys by logged user")
        .RequireAuthorization("authenticated");
    }
}
