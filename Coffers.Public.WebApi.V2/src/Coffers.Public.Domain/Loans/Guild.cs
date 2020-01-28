using System;

namespace Coffers.Public.Domain.Loans
{
    internal sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        public Guid? TariffId { get; set; }
        /// <summary>
        /// Тариф гильдии
        /// </summary>
        public Tariff Tariff { get; set; }
    }
}
