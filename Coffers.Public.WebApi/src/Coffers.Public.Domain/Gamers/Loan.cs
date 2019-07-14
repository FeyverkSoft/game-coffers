using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Уже было выплаченно в пользу займа
        /// </summary>
        public Decimal RedemptionAmount { get; set; }


        public LoanStatus LoanStatus { get; set; }
    }


}
