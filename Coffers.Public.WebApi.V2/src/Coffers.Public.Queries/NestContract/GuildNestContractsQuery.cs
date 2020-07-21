using System;
using System.Collections.Generic;
using System.Text;

using Query.Core;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class GuildNestContractsQuery : IQuery<IDictionary<String, IEnumerable<GuildNestContractView>>>
    {
        public Guid GuildId { get; }

        public GuildNestContractsQuery(Guid guildId)
            => (GuildId)
                = (guildId);
    }
}
