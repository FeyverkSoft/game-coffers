
using System;
using System.Collections.Generic;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public class GuildTariff
    {
        /// <summary>
        /// Идентификатор тарифа гильдии
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Тариф для главы
        /// </summary>
        public Tariff LeaderTariff { get; private set; }
        /// <summary>
        /// Тариф для офицера
        /// </summary>
        public Tariff OfficerTariff { get; private set; }
        /// <summary>
        /// Тариф для ветерана
        /// </summary>
        public Tariff VeteranTariff { get; private set; }
        /// <summary>
        /// Тариф для солдата
        /// </summary>
        public Tariff SoldierTariff { get; private set; }
        /// <summary>
        /// Тариф для духа
        /// </summary>
        public Tariff BeginnerTariff { get; private set; }
    }

    public sealed class Tariff
    {
        public Guid Id { get; private set; }

        public Decimal LoanTax { get; private set; }

        public Decimal ExpiredLoanTax { get; private set; }

        public IList<Decimal> Tax { get; private set; }
    }
}
