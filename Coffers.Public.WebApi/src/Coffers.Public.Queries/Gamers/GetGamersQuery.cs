using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;
using Query.Core;

namespace Coffers.Public.Queries.Gamers
{
    /// <summary>
    /// Запрос на получение списка игроков в гильдии с применением указанных фильтров
    /// </summary>
    public sealed class GetGamersQuery : IQuery<ICollection<GamersListView>>
    {
        /// <summary>
        /// Guild id
        /// </summary>
        public Guid GuildId { get; set; }
        /// <summary>
        /// Date to
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// Date from
        /// </summary>
        public DateTime? DateTo { get; set; }
        /// <summary>
        /// Gamer statuses list
        /// </summary>
        public List<GamerStatus> GamerStatuses { get; set; }
    }
}
