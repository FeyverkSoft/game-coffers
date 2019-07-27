using System;
using Coffers.Helpers;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public sealed class PutLoanBinding
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Описание, для чего был взят займ
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; set; }
    }

    public class PutLoanBindingValidator : AbstractValidator<PutLoanBinding>
    {
        public PutLoanBindingValidator()
        {
            RuleFor(r => r.Amount)
                .GreaterThan(0)
                .LessThan(int.MaxValue);// 2 лярда это и так много

            RuleFor(r => r.ExpiredDate)
                .NotEmpty()
                .GreaterThan(DateTime.UtcNow.Trunc(DateTruncType.Day));

            RuleFor(r => r.BorrowDate)
                .NotEmpty();


            RuleFor(r => r.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(512);
        }
    }
}
