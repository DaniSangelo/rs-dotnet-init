using ProductClientHub.API.Infra;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Delete
{
    public class DeleteProductUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();
            var product = dbContext.Products.FirstOrDefault(p => p.Id == id) ?? throw new NotFoundException("Product not found");
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }
    }
}
