using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infra;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.GetById
{
    public class GetClientByIdUseCase
    {
        public ResponseClientJson Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();
            var client = dbContext
                .Clients
                .Include(client => client.Products)
                .FirstOrDefault(c => c.Id == id) ?? throw new NotFoundException("Client not found");
            return new ResponseClientJson
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Products = client.Products.Select(p => new ResponseShortProductJson
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToList()
            };
        }
    }
}
