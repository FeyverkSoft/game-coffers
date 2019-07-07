using System;
using Coffers.Types.Guilds;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Guild
{
    public class GuildCreateBinding
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование гильдии
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Статус гильдии
        /// </summary>
        public GuildStatus Status { get; set; }
        /// <summary>
        /// Статус набора в гильдию
        /// </summary>
        public RecruitmentStatus RecruitmentStatus { get; set; }
    }

    public class GuildCreateBindingValidator : AbstractValidator<GuildCreateBinding>
    {
        public GuildCreateBindingValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(r => r.Status)
                .NotNull();

            RuleFor(r => r.RecruitmentStatus)
                .NotNull();

        }
    }
}
