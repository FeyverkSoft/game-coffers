using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Profiles
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
        /// Имя игрока
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; private set; }

        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; private set; }

        public List<Loan> Loans { get; private set; }

        public List<Penalty> Penalties { get; private set; }
    }
}
