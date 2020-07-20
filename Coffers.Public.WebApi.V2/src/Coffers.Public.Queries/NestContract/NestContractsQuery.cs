using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class NestContractsQuery : IQuery<IEnumerable<NestContractView>>
    {
        public Guid GuildId { get; }
        public Guid UserId { get; }

        public NestContractsQuery(Guid guildId, Guid userId) =>
            (GuildId, UserId) =
            (guildId, userId);
    }
}