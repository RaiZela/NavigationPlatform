namespace Journey.API.Endpoints;

//Acepts pagination parameters
//Construscts a GetJourneysQuery with these parameters
//Retrieves the data and returns it in a paginated format

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
.WithDescription("Get Paginated Journeys"); 
    }
}
