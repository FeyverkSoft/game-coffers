using System;
using Query.Core;

namespace Coffers.Public.Queries.Profiles
{
    /// <summary>
    /// Запрос на получение базовой информации об игроке по его id
    /// а так же некоторой статистики
    /// Такой как сумма активных займов, сумма активных штрафов, итд...
    /// </summary>
    public sealed class GetBaseGamerInfoQuery : IQuery<BaseGamerInfoView>
    {
        /// <summary>
        /// Идетификатор игрока по которому необходимо извлечь информацию
        /// </summary>
        public Guid UserId { get; set; }
    }
}