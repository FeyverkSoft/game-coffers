using System;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    /// <summary>
    /// запрос на получение счёта ги
    /// </summary>
    public sealed class GuildAccountQuery : IQuery<GuildAccountView>
    {
        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; set; }
    }
}
