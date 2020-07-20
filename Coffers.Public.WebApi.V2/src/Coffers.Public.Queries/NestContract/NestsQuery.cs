using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class NestsQuery : IQuery<IEnumerable<NestView>>
    {
        public Guid GuildId { get; }

        public NestsQuery(Guid guildId) =>
            (GuildId) =
            (guildId);
    }
}