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
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }
        /// <summary>
        /// Buhjr
        /// </summary>
        public User User { get; }

        public Guid? TariffId { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; } = 0;

        public LoanStatus LoanStatus { get; private set; } = LoanStatus.Active;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();
        public Boolean IsActive => LoanStatus == LoanStatus.Active ||
                                   LoanStatus == LoanStatus.Expired;

        protected Loan() { }
        public Loan(Guid id, Guid userId, Guid? tariffId, string description, DateTime expiredDate, decimal amount, decimal taxAmount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Non-negative number required");
            if (taxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(taxAmount), "Non-negative number required");

            Id = id;
            UserId = userId;
            TariffId = tariffId;
            Description = description;
            ExpiredDate = expiredDate;
            Amount = amount;
            TaxAmount = taxAmount;
        }

        public void MakeCancel()
        {
            if (LoanStatus == LoanStatus.Canceled)
                return;

            if (!IsActive)
                throw new InvalidOperationException($"Incorrect cuccent loan state; State:{LoanStatus}; Id:{Id}");

            LoanStatus = LoanStatus.Canceled;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }


        internal void MakePaid()
        {
            if (IsActive)
                throw new InvalidOperationException($"Incorrect cuccent loan state; State:{LoanStatus}; Id:{Id}");

            LoanStatus = LoanStatus.Paid;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
