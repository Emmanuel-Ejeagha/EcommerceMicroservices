namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName);
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}",
            async (string userName, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName), cancellationToken);

            var response = result.Adapt<GetBasketResponse>(); 

            return Results.Ok(response);
        })
        .WithName("Get Basket By Username")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get the shopping cart for a specific user.")
        .WithDescription("Retrieves the shopping cart associated with the provided username. " +
        "   If the user does not have a shopping cart, an empty cart will be returned.");
    }
}
