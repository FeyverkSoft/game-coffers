using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildRoleListQuery : IQuery<ICollection<GuildRoleView>>
    {
        public Guid GuildId { get; }
        public GuildRoleListQuery(Guid guildId) =>
            (GuildId) = (guildId);
    }
}
