using System;
using Coffers.Types.Account;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Operations
{
    public class OperationBinding
    {
        public Guid DocumentId { get; set; }

        public OperationType Type { get; set; }
    }
    public class OperationBindingValidator : AbstractValidator<OperationBinding>
    {
        public OperationBindingValidator()
        {

            RuleFor(r => r.DocumentId)
                .NotEmpty();
        }
    }
}
