using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Guilds
{
    /// <summary>
    /// Возвращает гильдию с подробной информацией по её ID и id пользователя состоящего в ней
    /// </summary>
    public class GuildQuery : IQuery<GuildView>
    {
        /// <summary>
        /// ID гильдии
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя, для контроля того что он состоит в этой гильдии
        /// </summary>
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// Возвращает список гильдий
    /// </summary>
    public class GuildsQuery : IQuery<ICollection<GuildView>>
    {
    }
}
