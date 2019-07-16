using System;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    /// <summary>
    /// запрос на получение актуальных балансов гильдии
    /// </summary>
    public sealed class GuildBalanceQuery : IQuery<GuildBalanceView>
    {
        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; set; }
    }
}
