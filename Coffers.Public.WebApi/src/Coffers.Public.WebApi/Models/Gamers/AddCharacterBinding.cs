﻿using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public class AddCharacterBinding
    {
        /// <summary>
        /// Character name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Character class name
        /// </summary>
        public String ClassName { get; set; }
    }
    public class AddCharacterBindingValidator : AbstractValidator<AddCharacterBinding>
    {
        public AddCharacterBindingValidator()
        {
            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(r => r.ClassName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}