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
        public Guid Id { get; private set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; private set; }

        public Account Account { get; private set; }

        public Guid TariffId { get; private set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; private set; }

        /// <summary>
        /// Дата создания займа
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; private set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; private set; }

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; private set; }

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; private set; }
        /// <summary>
        /// Статус займа
        /// </summary>
        public LoanStatus LoanStatus { get; private set; }
        internal void SetStatus(LoanStatus newStatus)
        {
            if (LoanStatus != newStatus)
            {
                UpdateDate = DateTime.UtcNow;
                LoanStatus = newStatus;
                ConcurrencyTokens = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; }

        public Loan(Guid id, Guid tariffId, Decimal amount, Decimal taxAmount,
            String description, DateTime borrowDate, DateTime expiredDate)
        {
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            LoanStatus = LoanStatus.Active;
            ConcurrencyTokens = Guid.NewGuid();
            Id = id;
            Account = new Account();
            TariffId = tariffId;
            Amount = amount;
            TaxAmount = taxAmount;
            Description = description;
            BorrowDate = borrowDate;
            ExpiredDate = expiredDate;
        }
    }


}
