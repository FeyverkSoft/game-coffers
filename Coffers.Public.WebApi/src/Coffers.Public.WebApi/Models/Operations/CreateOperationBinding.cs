
using System;
using Coffers.Types.Account;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Operations
{
    public sealed class CreateOperationBinding
    {
        /// <summary>
        /// С кого
        /// </summary>
        public Guid FromUserId { get; set; }
        /// <summary>
        /// На кого
        /// </summary>
        public Guid ToUserId { get; set; }
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
    }

    public class CreateOperationBindingValidator : AbstractValidator<CreateOperationBinding>
    {
        public CreateOperationBindingValidator()
        {

        }
    }
}
