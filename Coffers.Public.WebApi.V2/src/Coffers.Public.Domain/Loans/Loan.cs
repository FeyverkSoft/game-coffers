using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Loans
{
    /// <summary>
    /// Сущность хранит займы игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// игрок
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тариф по которому проходит займ
        /// </summary>
        public Tariff Tariff { get; set; }
        public Guid TariffId { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; set; }

        public LoanStatus LoanStatus { get; set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }
    }
}
