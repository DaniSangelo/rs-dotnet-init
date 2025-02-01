using ProductClientHub.Exceptions.ExceptionsBase;
using ProductClientHub.API.Infra;
using ProductClientHub.Communication.Requests;
using ProductClientHub.API.UseCases.Clients.SharedValidator;

namespace ProductClientHub.API.UseCases.Clients.Update
{
    public class UpdateClientUseCase
    {
        public void Execute(Guid clientId, RequestClientJson request)
        {
            Validate(request);
            var dbContext = new ProductClientHubDbContext();
            var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId) ?? throw new NotFoundException("Client not found");

            client.Name = request.Name;
            client.Email = request.Email;

            dbContext.Clients.Update(client);
            dbContext.SaveChanges();
        }

        private static void Validate(RequestClientJson request)
        {
            var validator = new RequestClientValidator();
            var result = validator.Validate(request);
            if (result.IsValid == false)
            { 
                var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
