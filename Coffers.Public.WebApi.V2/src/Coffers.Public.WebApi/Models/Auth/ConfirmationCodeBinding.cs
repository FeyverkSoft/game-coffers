using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class ConfirmationCodeBinding
    {
        public String ConfirmationCode { get; set; }
    }

    public class ConfirmationCodeBindingValidator : AbstractValidator<ConfirmationCodeBinding>
    {
        public ConfirmationCodeBindingValidator()
        {
            RuleFor(r => r.ConfirmationCode)
                .NotNull()
                .NotEmpty();
        }
    }
}