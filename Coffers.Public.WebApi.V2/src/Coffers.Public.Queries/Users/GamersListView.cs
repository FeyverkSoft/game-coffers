using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Users
{
    /// <summary>
    /// base gamers info
    /// </summary>
    public sealed class GamersListView
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Characters list
        /// </summary>
        public IEnumerable<CharacterView> Characters { get; }

        /// <summary>
        /// User balance
        /// </summary>
        public Decimal Balance { get; }

        /// <summary>
        /// Список штрафов
        /// </summary>
        public IEnumerable<PenaltyView> Penalties { get; }

        /// <summary>
        /// Список займов
        /// </summary>
        public IEnumerable<LoanView> Loans { get; }

        /// <summary>
        /// Rank
        /// </summary>
        public GamerRank Rank { get; }

        /// <summary>
        /// Статус игрока
        /// </summary>
        public GamerStatus Status { get; }
        /// <summary>
        /// День рождения
        /// </summary>
        public DateTime DateOfBirth { get; }
        /// <summary>
        /// имя юзвера
        /// </summary>
        public String Name { get; }

        public GamersListView(Guid id, String name, Decimal balance,
            IEnumerable<CharacterView> characters, GamerRank rank, GamerStatus status,
            DateTime dateOfBirth, IEnumerable<PenaltyView> penalties, IEnumerable<LoanView> loans)
            => (Id, Name, Balance, Characters, Rank, Status, DateOfBirth, Penalties, Loans)
            = (id, name, balance, characters, rank, status, dateOfBirth, penalties, loans);
    }
}