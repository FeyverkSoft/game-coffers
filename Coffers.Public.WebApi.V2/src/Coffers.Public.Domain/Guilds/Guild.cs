using System;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; set; }

        public Guid ConcurrencyTokens { get; set; }
    }
}
