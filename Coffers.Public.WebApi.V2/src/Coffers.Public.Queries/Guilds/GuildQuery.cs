using System;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildQuery : IQuery<GuildView>
    {
        public Guid GuildId { get; }
        public GuildQuery(Guid guildId) =>
            (GuildId) = (guildId);
    }
}
