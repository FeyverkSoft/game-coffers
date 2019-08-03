using System;
using Coffers.Types.Account;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Operations
{
    public sealed class CreateOperationBinding
    {
        /// <summary>
        /// Id операции
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Пользователь с которого производится перевод
        /// </summary>
        public Guid? FromUserId { get; set; }
        /// <summary>
        /// Пользователь в пользу которого производится перевод
        /// </summary>
        public Guid? ToUserId { get; set; }
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Идентификатор штрафа, только при типе операции Штраф
        /// </summary>
        public Guid? PenaltyId { get; set; }
        /// <summary>
        /// Идентификатор оплачиваемого займа, только при типе операции Займ
        /// </summary>
        public Guid? LoanId { get; set; }
    }

    public class CreateOperationBindingValidator : AbstractValidator<CreateOperationBinding>
    {
        public CreateOperationBindingValidator()
        {
            RuleFor(b => b.Id)
                .NotNull()
                .NotEmpty();

            When(x => x.Type == OperationType.Loan, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
                RuleFor(b => b.LoanId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.Penalty, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
                RuleFor(b => b.PenaltyId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.Tax, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
                RuleFor(b => b.FromUserId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.Exchange, () =>
            {
                RuleFor(b => b.FromUserId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.Output, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
                RuleFor(b => b.ToUserId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.InternalOutput, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
                RuleFor(b => b.ToUserId)
                    .NotNull()
                    .NotEmpty();
            });
            When(x => x.Type == OperationType.Emission, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
            });
            When(x => x.Type == OperationType.InternalEmission, () =>
            {
                RuleFor(b => b.FromUserId)
                    .NotNull()
                    .NotEmpty();
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
            });
            When(x => x.Type == OperationType.Sell, () =>
            {
                RuleFor(b => b.Amount)
                    .NotNull()
                    .GreaterThanOrEqualTo(0m);
            });

            RuleFor(b => b.Description)
                .NotNull()
                .NotEmpty();
        }
    }
}
