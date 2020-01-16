using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;
using Query.Core;

namespace Coffers.Public.Queries.Users
{
    public sealed class GetGamersQuery : IQuery<ICollection<GamersListView>>
    {
        public Guid GuildId { get; }
        /// <summary>
        /// Date to
        /// </summary>
        public DateTime? DateMonth { get; }

        /// <summary>
        /// Gamer statuses list
        /// </summary>
        public ICollection<GamerStatus> GamerStatuses { get; }

        public GetGamersQuery(Guid guildId, DateTime? dateMonth, ICollection<GamerStatus> gamerStatuses)
            => (GuildId, DateMonth, GamerStatuses)
            =  (guildId, dateMonth, gamerStatuses ?? new List<GamerStatus>());
    }
}
