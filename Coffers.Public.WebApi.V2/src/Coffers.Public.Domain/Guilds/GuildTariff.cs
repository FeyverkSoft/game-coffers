
using System;

namespace Coffers.Public.Domain.Guilds
{
    public class GuildTariff
    {
        /// <summary>
        /// Идентификатор тарифа гильдии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания тарифа
        /// /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Тариф для главы
        /// </summary>
        public Tariff LeaderTariff { get; set; }
        /// <summary>
        /// Тариф для офицера
        /// </summary>
        public Tariff OfficerTariff { get; set; }
        /// <summary>
        /// Тариф для ветерана
        /// </summary>
        public Tariff VeteranTariff { get; set; }
        /// <summary>
        /// Тариф для солдата
        /// </summary>
        public Tariff SoldierTariff { get; set; }
        /// <summary>
        /// Тариф для духа
        /// </summary>
        public Tariff BeginnerTariff { get; set; }
    }

    public class Tariff
    {
        public Guid Id { get; set; }

        public Decimal LoanTax { get; set; }

        public Decimal ExpiredLoanTax { get; set; }

        public String Tax { get; set; }
    }
}
