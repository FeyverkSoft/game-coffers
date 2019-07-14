using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Gamers
{
    /// <summary>
    /// Basic gamer information.
    /// </summary>
    public sealed class BaseGamerInfoView
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// User Current balance
        /// </summary>
        public Decimal Balance { get; set; }
        /// <summary>
        /// Amount of active loans
        /// </summary>
        public Decimal ActiveLoanAmount { get; set; }
        /// <summary>
        /// Amount of active penalty
        /// </summary>
        public Decimal ActivePenaltyAmount { get; set; }
        /// <summary>
        /// Gamer guild rank
        /// </summary>
        public GamerRank Rank { get; set; }
        /// <summary>
        /// Characters count
        /// </summary>
        public Int32 CharCount { get; set; }
    }
}