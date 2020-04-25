using System;
using Query.Core;

namespace Coffers.Public.Queries.Operations
{
    public sealed class GetOperationQuery : IQuery<OperationView>
    {
        public Guid GuildId { get; }

        /// <summary>
        /// Operation Id
        /// </summary>
        public Guid OperationId { get; }

        public GetOperationQuery(Guid guildId, Guid operationId)
            => (GuildId, OperationId)
                = (guildId, operationId);
    }
}