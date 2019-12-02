using System;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildAccountView
    {
        /// <summary>
        /// Нормер счёта гильдии
        /// </summary>
        public Guid AccountId { get; private set; }

        public GuildAccountView(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
