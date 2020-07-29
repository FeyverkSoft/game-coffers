using System;

using FluentValidation;

namespace Coffers.Public.WebApi.Models.Auth
{
    public class AuthEmailBinding
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Имя/Ник пользователя
        /// </summary> 
        public String Name { get; set; }
    }
    public class AuthEmailBindingValidator : AbstractValidator<AuthEmailBinding>
    {
        public AuthEmailBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotNull()
                .NotEmpty();

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
