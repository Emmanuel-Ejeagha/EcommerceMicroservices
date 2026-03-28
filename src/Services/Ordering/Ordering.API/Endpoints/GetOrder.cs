using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetQueries;

namespace Ordering.API.Endpoints;

//- Accept pagination parametrers
//- Constructs a GetOrderQuery with these paramaters.
//- Retrieves the data and returns it in a paginated format.

//public record GetOrdersRequest(PaginationRequest PaginationRequest);

public record GetOrdersResponse(PaginatedResult<OrderDto> Order);
public class GetOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async([AsParameters] PaginationRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request), cancellationToken);

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}
