using System;
using Coffers.Types.Account;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Operations
{
    public class OperationBinding
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; set; }

        public Guid OperationId { get; set; }

        public OperationType Type { get; set; }
    }
    public class OperationBindingValidator : AbstractValidator<OperationBinding>
    {
        public OperationBindingValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty();

            RuleFor(r => r.OperationId)
                .NotEmpty();
        }
    }
}
