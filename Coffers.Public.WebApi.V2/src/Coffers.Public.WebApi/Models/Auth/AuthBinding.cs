using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class AuthBinding
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public String Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public String Password { get; set; }
    }
    public class AuthBindingValidator : AbstractValidator<AuthBinding>
    {
        public AuthBindingValidator()
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
