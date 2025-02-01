using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Clients.SharedValidator
{
    public class RequestClientValidator : AbstractValidator<RequestClientJson>
    {
        public RequestClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(client => client.Email).EmailAddress().WithMessage("Invalid email");
        }
    }
}
