using System;
using Coffers.Types.Gamer;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Guild
{
    public class GamerCreateBinding
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Логин по которому игрок будет входить
        /// </summary>
        public String Login { get; set; }
    }

    public class GamerCreateBindingValidator : AbstractValidator<GamerCreateBinding>
    {
        public GamerCreateBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(r => r.Login)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(r => r.DateOfBirth)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.Status)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.Rank)
                .NotNull()
                .NotEmpty();
        }
    }
}
