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
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Дата создания займа
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Уже было выплаченно в пользу займа
        /// </summary>
        public Decimal RepaymentAmount { get; set; }
        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; set; }

        public LoanStatus LoanStatus { get; set; }
    }


}
