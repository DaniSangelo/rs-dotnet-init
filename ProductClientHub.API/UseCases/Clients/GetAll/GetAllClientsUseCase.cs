using ProductClientHub.API.Infra;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.GetAll
{
    public class GetAllClientsUseCase
    {
        public ResponseAllClientsJson Execute()
        {
            var dbContext = new ProductClientHubDbContext();

            var clients = dbContext.Clients.ToList();

            return new ResponseAllClientsJson
            {
                Clients = clients.Select(x => new ResponseShortClientJson
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList()
            };
        }
    }
}
