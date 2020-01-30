using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Loan
{
    public class AddLoanBinding
    {
        /// <summary>
        /// unique loan identifier 
        /// </summary>
        public Guid LoanId { get; set; }
        /// <summary>
        /// unique user identifier
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Loan amount
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Loan description
        /// </summary>
        public String Description { get; set; }
    }
    public class AddLoanBindingValidator : AbstractValidator<AddLoanBinding>
    {
        public AddLoanBindingValidator()
        {
            RuleFor(r => r.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2048);

            RuleFor(r => r.UserId)
                .NotEmpty();

            RuleFor(r => r.LoanId)
                .NotEmpty();

            RuleFor(r => r.Amount)
                .GreaterThan(0);
        }
    }
}
