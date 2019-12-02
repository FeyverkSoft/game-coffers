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
        public GamersListView(
            Guid id,
            String name,
            Decimal balance,
            List<CharacterView> characters,
            GamerRank rank,
            GamerStatus status,
            DateTime dateOfBirth,
            List<PenaltyView> penalties,
            List<LoanView> loans)
        {
            Id = id;
            Name = name;
            Balance = balance;
            Characters = characters;
            Rank = rank;
            Status = status;
            DateOfBirth = dateOfBirth;
            Penalties = penalties;
            Loans = loans;
        }

        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Characters list
        /// </summary>
        public ICollection<CharacterView> Characters { get; }

        /// <summary>
        /// User balance
        /// </summary>
        public Decimal Balance { get; }

        /// <summary>
        /// Список штрафов
        /// </summary>
        public List<PenaltyView> Penalties { get; }

        /// <summary>
        /// Список займов
        /// </summary>
        public List<LoanView> Loans { get; }

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
    }
}
