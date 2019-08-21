using System;
using Query.Core;

namespace Coffers.Public.Queries.Gamers
{
    /// <summary>
    /// Запрос на получение 
    /// Дефолтного номера счета игрока, 
    /// id гильдии игрока
    /// по идентификатору игрока
    /// </summary>
    public sealed class GetGamerInfoQuery : IQuery<GamerInfoView>
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; set; }
    }
}