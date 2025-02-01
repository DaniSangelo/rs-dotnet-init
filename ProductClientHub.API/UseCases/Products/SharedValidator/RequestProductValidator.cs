using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator
{
    public class RequestProductValidator : AbstractValidator<RequestProductJson>
    {
        public RequestProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Product name must be informed");
            RuleFor(product => product.Brand).NotEmpty().WithMessage("Product brand must be informed");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Product price must be greater than zero");
        }
    }
}
