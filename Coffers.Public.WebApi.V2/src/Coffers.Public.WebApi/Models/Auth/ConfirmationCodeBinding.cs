using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class ConfirmationCodeBinding
    {
        [FromQuery]
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