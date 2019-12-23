using System;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// тариф гильдии, действующий на данный момент
        /// </summary>
        public GuildTariff Tariff { get; set; }
        public Guid? TariffId { get; set; }

        public Guid ConcurrencyTokens { get; set; }
    }
}
