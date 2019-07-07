using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class PasswordBinding
    {
        public String Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public String Password { get; set; }
    }
    public class PasswordBindingValidator : AbstractValidator<PasswordBinding>
    {
        public PasswordBindingValidator()
        {
            RuleFor(r => r.Login)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);
        }
    }
}
