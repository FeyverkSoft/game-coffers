using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; private set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; private set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; private set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; private set; }

        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Loan> Loans { get; private set; }
    }
}
