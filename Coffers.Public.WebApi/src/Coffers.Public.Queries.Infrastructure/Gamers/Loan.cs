using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; private set; }

        public Account Account { get; private set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; private set; }

        /// <summary>
        /// Дата создания займа
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; private set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; private set; }

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; private set; }

        public LoanStatus LoanStatus { get; private set; }

    }
}
