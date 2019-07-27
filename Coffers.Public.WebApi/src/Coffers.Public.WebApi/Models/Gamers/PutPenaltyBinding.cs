using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public sealed class PutPenaltyBinding
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// Сумма штрафа
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; set; }
    }
    public class PutPenaltyBindingValidator : AbstractValidator<PutPenaltyBinding>
    {
        public PutPenaltyBindingValidator()
        {
            RuleFor(r => r.Amount)
                .GreaterThan(0)
                .LessThan(int.MaxValue);// 2 лярда это и так много

            RuleFor(r => r.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2048);
        }
    }
}
