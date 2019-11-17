using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public class EditCharacterBinding
    {

        /// <summary>
        /// Character name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Character class name
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// Is main character
        /// </summary>
        public Boolean IsMain { get; set; } = false;

    }
    public class EditCharacterBindingValidator : AbstractValidator<EditCharacterBinding>
    {
        public EditCharacterBindingValidator()
        {
            RuleFor(r => r.Name)
                .MaximumLength(64);

            RuleFor(r => r.ClassName)
                .MaximumLength(64);
        }
    }
}