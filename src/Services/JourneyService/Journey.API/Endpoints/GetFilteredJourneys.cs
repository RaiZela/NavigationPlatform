using Journey.Application.Journeys.Queries.GetFilteredJourneys;

namespace Journey.API.Endpoints;

public record GetJourneysFilteredResponse(PaginatedResult<JourneyDto> Journeys);
public class GetFilteredJourneys : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("journeys/filtered", async ([AsParameters] GetFilteredJourneysQuery request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return Results.Ok(result.Adapt<GetJourneysFilteredResponse>());
        })
        .WithName("Get Filtered Journeys")
        .Produces<GetJourneysFilteredResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Filtered Journeys")
        .WithDescription("Get Filtered Journeys")
        .RequireAuthorization("authenticated");
    }
}
