using FluentValidation;

namespace Coffers.Public.WebApi.Models.Clients
{
    public class ClientCreateBinding
    {
        public string Id { get; set; }
    }

    public class ClientCreateBindingValidator : AbstractValidator<ClientCreateBinding>
    {
        public ClientCreateBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}
