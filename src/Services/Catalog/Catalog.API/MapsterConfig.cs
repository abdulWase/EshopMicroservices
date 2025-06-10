using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductById;

namespace Catalog.API
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<GetProductByIdResult, GetProductByIdResponse>.NewConfig()
                        .Map(dest => dest.Product, src => src.Product);
            TypeAdapterConfig<DeleteProductResult, DeleteProductResponse>.NewConfig()
                        .Map(dest => dest.isSuccess, src => src.IsSuccess);
        }
    }
}
