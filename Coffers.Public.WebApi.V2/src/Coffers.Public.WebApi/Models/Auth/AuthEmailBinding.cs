using System;

using FluentValidation;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class AuthEmailBinding
    {
        /// <summary>
        /// Email пользователя
        /// </summary>
        public String Email { get; set; }
        public Guid GuildId { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public String Password { get; set; }
    }
    public class AuthEmailBindingValidator : AbstractValidator<AuthEmailBinding>
    {
        public AuthEmailBindingValidator()
        {
            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64)
                .EmailAddress();

            RuleFor(r => r.GuildId)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);
        }
    }
}
