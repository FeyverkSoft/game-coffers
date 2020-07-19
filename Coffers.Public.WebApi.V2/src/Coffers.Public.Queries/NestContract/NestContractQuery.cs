using System;
using Query.Core;

namespace Coffers.Public.Queries.NestContract
{
    public sealed class NestContractQuery : IQuery<NestContractView>
    {
        public Guid NestContractId { get; }
        public Guid GuildId { get; }

        public NestContractQuery(Guid nestContractId, Guid guildId)
            => (NestContractId, GuildId)
                = (nestContractId, guildId);
    }
}