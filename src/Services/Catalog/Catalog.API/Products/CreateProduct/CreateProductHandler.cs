

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category,string Description,string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) //IDocumetSession is an abstraction no need for Repository Pattern
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Create Product entity from command object
            //save to database
            //return CreateProducResult result
            var product = new Product { 
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //save to database
            session.Store(product);
           await session.SaveChangesAsync(cancellationToken);
            //rturn result
            return new CreateProductResult(product.Id);
        }
    }
}
