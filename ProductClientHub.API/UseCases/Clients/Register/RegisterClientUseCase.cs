using ProducClientHub.Exceptions.ExceptionsBase;
using ProductClientHub.API.Entities;
using ProductClientHub.API.Infra;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterClientUseCase
    {
        public ResponseShortClientJson Execute(RequestClientJson request)
        {
            Validate(request);
            var dbContext = new ProductClientHubDbContext();
            var client = new Client
            {
                Email = request.Email,
                Name = request.Name,
            };
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            return new ResponseShortClientJson
            {
                Name = client.Name,
                Id = client.Id,
            };
        }

        private static void Validate(RequestClientJson request)
        {
            var validator = new RegisterClientValidator();
            var result = validator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
