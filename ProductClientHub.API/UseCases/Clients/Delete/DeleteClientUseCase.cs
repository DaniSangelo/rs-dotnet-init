using ProductClientHub.API.Infra;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Delete
{
    public class DeleteClientUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();
            var client = dbContext.Clients.FirstOrDefault(c => c.Id == id) ?? throw new NotFoundException("Client not found");
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }
    }
}
