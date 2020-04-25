using System;
using Coffers.Types.Gamer;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Guild
{
    public class UpdateUserRoleBinding
    {
        /// <summary>
        /// Тариф для игрока с этим рангом
        /// </summary>
        public TariffBinding Tariff { get; set; }
    }

    public class TariffBinding
    {
        public Decimal LoanTax { get; set; }

        public Decimal ExpiredLoanTax { get; set; }

        public Decimal[] Tax { get; set; }
    }

    public class UpdateTariffBindingValidator : AbstractValidator<UpdateUserRoleBinding>
    {
        public UpdateTariffBindingValidator()
        {
            RuleFor(b => b.Tariff)
                .NotNull();

            When(x => x.Tariff != null, () =>
            {
                RuleForEach(b => b.Tariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.Tariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.Tariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
        }
    }
}
