
namespace Journey.API.Endpoints;

[Authorize]
public record GetLoggedUserJourneysResponse(IEnumerable<JourneyDto> Journey);
public class GetLoggedUserJourneys : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/journeys/journeys-by-logged-user", async (ISender sender) =>
        {
            var result = await sender.Send(new GetLoggedUserJourneysQuery());
            return Results.Ok(result);
        })
            .WithName("GetJourneysByLoggedUser")
.Produces<IEnumerable<GetJourneysByUserResponse>>(StatusCodes.Status201Created)
.ProducesProblem(StatusCodes.Status400BadRequest)
.WithSummary("Get Journeys by logged ser")
.WithDescription("Get Journeys by logged user");
    }
}
