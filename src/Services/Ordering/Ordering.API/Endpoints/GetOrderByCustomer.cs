using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints;

//Accept a name parameter.
//- Constructs a GetOrderByCustomerQuery
//- Retrieves and returns matching orders

//public record GetOrderByCustomerRequest(Guid CustomerId);

public record GetOrderByCustomerResponse(Guid CustomerId);

public class GetOrderByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetOrderByCustomerQuery(customerId), cancellationToken);

            var response = result.Adapt<GetOrderByCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrderByCustomer")
        .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By Customer");
    }
}
