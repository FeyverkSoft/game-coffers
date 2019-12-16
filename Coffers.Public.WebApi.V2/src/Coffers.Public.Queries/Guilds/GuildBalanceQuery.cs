using System;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildBalanceQuery : IQuery<GuildBalanceView>
    {
        public Guid GuildId { get; }

        public GuildBalanceQuery(Guid guildId)
            => (GuildId)
             = (guildId);
    }
}
