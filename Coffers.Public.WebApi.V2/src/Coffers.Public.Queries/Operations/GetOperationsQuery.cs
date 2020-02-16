using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Operations
{
    public sealed class GetOperationsQuery : IQuery<ICollection<OperationListView>>
    {
        public Guid GuildId { get; }
        /// <summary>
        /// Date to
        /// </summary>
        public DateTime? DateMonth { get; }

        public GetOperationsQuery(Guid guildId, DateTime? dateMonth)
            => (GuildId, DateMonth)
                = (guildId, dateMonth);
    }
}
