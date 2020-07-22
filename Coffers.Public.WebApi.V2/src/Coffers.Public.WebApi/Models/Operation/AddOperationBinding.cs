using System;
using Coffers.Types.Account;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Operation
{
    public sealed class AddOperationBinding
    {

        /// <summary>
        /// Идентификатор операци
        /// </summary>
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        /// <summary>
        /// Ссылка на родительскую проводку
        /// </summary>
        public Guid? ParentOperationId { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String? Description { get; set; }
    }

    public class AddOperationBindingValidator : AbstractValidator<AddOperationBinding>
    {
        public AddOperationBindingValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty();
            RuleFor(r => r.Id)
                .NotEmpty();

            RuleFor(r => r.Amount)
                .NotEqual(0);
        }
    }
}
