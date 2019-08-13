using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Profiles
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
        /// Сумма активных займов, включая комиссию,налог и штраф за просрочку
        /// </summary>
        public Decimal ActiveLoanAmount { get; set; }
        /// <summary>
        /// Сумма штрафов по займам
        /// </summary>
        public Decimal ActiveExpLoanAmount { get; set; }
        /// <summary>
        /// Сумма комисси по активным займам
        /// </summary>
        public Decimal ActiveLoanTaxAmount { get; set; }
        /// <summary>
        /// Amount of active penalty
        /// </summary>
        public Decimal ActivePenaltyAmount { get; set; }
        /// <summary>
        /// Сумма выплаченная в пользу займов
        /// </summary>
        public Decimal RepaymentLoanAmount { get; set; }
        /// <summary>
        /// Сумма Сумма выплаченого налога
        /// </summary>
        public Decimal RepaymentTaxAmount { get; set; }
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