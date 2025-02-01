using ProductClientHub.API.Entities;
using ProductClientHub.API.Infra;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProductUseCase
    {
        public ResponseShortProductJson Execute(Guid clientId, RequestProductJson request)
        {
            var dbContext = new ProductClientHubDbContext();
            Validate(dbContext, clientId, request);

            var product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                ClientId = clientId,
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return new ResponseShortProductJson
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public static void Validate(ProductClientHubDbContext dbContext, Guid clientId, RequestProductJson request)
        {
            var clientExist = dbContext.Clients.Any(c => c.Id == clientId);
            if (!clientExist) throw new NotFoundException("Client does not exist");
            var validator = new RequestProductValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
