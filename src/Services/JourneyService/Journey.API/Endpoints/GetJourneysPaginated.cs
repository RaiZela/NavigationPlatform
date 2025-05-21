namespace Journey.API.Endpoints;

public record GetJourneysPaginatedResponse(PaginatedResult<JourneyDto> Journeys);
public class GetJourneysPaginated : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.Map("journeys", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {

        })
        .WithName("Get Journeys Paginated")
        .Produces<CreateJourneyResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Paginated Journeys")
        .WithDescription("Get Paginated Journeys")
        .RequireAuthorization("authenticated");
    }
}
