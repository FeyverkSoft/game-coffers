
using System;
using System.Collections.Generic;

namespace Coffers.Public.Domain.Guilds
{
    public class GuildTariff
    {
        /// <summary>
        /// Идентификатор тарифа гильдии
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Дата создания тарифа
        /// </summary>
        public DateTime CreateDate { get; internal set; }
        /// <summary>
        /// Тариф для главы
        /// </summary>
        public Tariff LeaderTariff { get; internal set; }
        /// <summary>
        /// Тариф для офицера
        /// </summary>
        public Tariff OfficerTariff { get; internal set; }
        /// <summary>
        /// Тариф для ветерана
        /// </summary>
        public Tariff VeteranTariff { get; internal set; }
        /// <summary>
        /// Тариф для солдата
        /// </summary>
        public Tariff SoldierTariff { get; internal set; }
        /// <summary>
        /// Тариф для духа
        /// </summary>
        public Tariff BeginnerTariff { get; internal set; }

        public GuildTariff()
        {
            CreateDate = DateTime.UtcNow;
        }
    }

    public sealed class Tariff
    {
        public Guid Id { get; private set; }

        public Decimal LoanTax { get; private set; }

        public Decimal ExpiredLoanTax { get; private set; }

        public IList<Decimal> Tax { get; private set; }

        internal Tariff() { }

        public Tariff(Decimal loanTax, Decimal expiredLoanTax, IList<Decimal> tax)
        {
            Id = Guid.NewGuid();
            LoanTax = loanTax;
            ExpiredLoanTax = expiredLoanTax;
            Tax = tax;
        }
    }
}
