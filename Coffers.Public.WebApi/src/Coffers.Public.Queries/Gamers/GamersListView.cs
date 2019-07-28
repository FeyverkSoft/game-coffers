using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Gamers
{
    /// <summary>
    /// base gamers info
    /// </summary>
    public sealed class GamersListView
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Characters list
        /// </summary>
        public ICollection<String> Characters { get; set; }

        /// <summary>
        /// User balance
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Список штрафов
        /// </summary>
        public List<PenaltyView> Penalties { get; set; }

        /// <summary>
        /// Список займов
        /// </summary>
        public List<LoanView> Loans { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        public GamerRank Rank { get; set; }

        /// <summary>
        /// Статус игрока
        /// </summary>
        public GamerStatus Status { get; set; }
        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}
