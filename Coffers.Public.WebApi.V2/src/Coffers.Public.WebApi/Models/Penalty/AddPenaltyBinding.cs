using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.User
{
    public class AddPenaltyBinding
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Penalty description
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Penalty amount
        /// </summary>
        public Decimal Amount { get; set; }
    }
    public class AddPenaltyBindingValidator : AbstractValidator<AddPenaltyBinding>
    {
        public AddPenaltyBindingValidator()
        {
            RuleFor(r => r.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2048);

            RuleFor(r => r.Amount)
                .GreaterThan(0);
        }
    }
}
