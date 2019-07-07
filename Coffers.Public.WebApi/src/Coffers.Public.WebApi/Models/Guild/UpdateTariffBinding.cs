using System;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Guild
{
    public class UpdateTariffBinding
    {
        /// <summary>
        /// Тариф для главы
        /// </summary>
        public TariffBinding LeaderTariff { get; set; }
        /// <summary>
        /// Тариф для офицера
        /// </summary>
        public TariffBinding OfficerTariff { get; set; }
        /// <summary>
        /// Тариф для ветерана
        /// </summary>
        public TariffBinding VeteranTariff { get; set; }
        /// <summary>
        /// Тариф для солдата
        /// </summary>
        public TariffBinding SoldierTariff { get; set; }
        /// <summary>
        /// Тариф для духа
        /// </summary>
        public TariffBinding BeginnerTariff { get; set; }
    }

    public class TariffBinding
    {
        public Decimal LoanTax { get; set; }

        public Decimal ExpiredLoanTax { get; set; }

        public Decimal[] Tax { get; set; }
    }

    public class UpdateTariffBindingValidator : AbstractValidator<UpdateTariffBinding>
    {
        public UpdateTariffBindingValidator()
        {
            When(x => x.BeginnerTariff != null, () =>
            {
                RuleForEach(b => b.BeginnerTariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.BeginnerTariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.BeginnerTariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
            When(x => x.SoldierTariff != null, () =>
            {
                RuleForEach(b => b.SoldierTariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.SoldierTariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.SoldierTariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
            When(x => x.VeteranTariff != null, () =>
            {
                RuleForEach(b => b.VeteranTariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.VeteranTariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.VeteranTariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
            When(x => x.OfficerTariff != null, () =>
            {
                RuleForEach(b => b.OfficerTariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.OfficerTariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.OfficerTariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
            When(x => x.LeaderTariff != null, () =>
            {
                RuleForEach(b => b.LeaderTariff.Tax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.LeaderTariff.LoanTax)
                    .GreaterThanOrEqualTo(0);
                RuleFor(b => b.LeaderTariff.ExpiredLoanTax)
                    .GreaterThanOrEqualTo(0);
            });
        }
    }
}
