using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Profile
{
    public class DeleteCharacterBinding
    {
        /// <summary>
        /// Character name
        /// </summary>
        public String Name { get; set; }

    }
    public class DeleteCharacterBindingValidator : AbstractValidator<DeleteCharacterBinding>
    {
        public DeleteCharacterBindingValidator()
        {
            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}
