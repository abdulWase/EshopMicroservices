
namespace Catalog.API.Products.GetProducts
{
    //We do not need a request Object because we are getting all Products
    //public record GetProductRequest();
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //ISender comes from Mediator
            app.MapGet("/products", async(ISender sender) => {
                var result = await sender.Send(new GetProductsQuery());
                //GetProductResponse should be similar to GetProductsResult in GetProductsHandler
                var response = result.Adapt<GetProductResponse>();//Using Mapster

                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}
