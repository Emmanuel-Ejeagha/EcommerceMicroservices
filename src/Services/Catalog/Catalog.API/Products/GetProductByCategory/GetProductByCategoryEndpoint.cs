namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryRequest()

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category?}", async (string? category, ISender sender, CancellationToken cancellationToken = default) =>
        {
            if (string.IsNullOrWhiteSpace(category))
                return Results.BadRequest("Category is required.");

            var result = await sender.Send(new GetProductByCategoryQuery(category));

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product by Category")
        .WithDescription("Get Product by Category");
    }
}
