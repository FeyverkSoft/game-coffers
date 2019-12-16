using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users
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
        /// Идентификатор игрока
        /// </summary>
        public User User { get; set; }

        public User UserId { get; set; }

        /// <summary>
        /// Тариф по которому проходит займ
        /// </summary>
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

        public LoanStatus LoanStatus { get; set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }

        internal void SetStatus(LoanStatus newStatus)
        {
            if (LoanStatus == newStatus)
                return;

            UpdateDate = DateTime.UtcNow;
            LoanStatus = newStatus;
            ConcurrencyTokens = Guid.NewGuid();
        }

        public Loan(Guid id, Guid tariffId, Decimal amount,
            String description, DateTime borrowDate, DateTime expiredDate)
        {
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            LoanStatus = LoanStatus.Active;
            ConcurrencyTokens = Guid.NewGuid();
            Id = id;
            TariffId = tariffId;
            Amount = amount;
            Description = description;
            BorrowDate = borrowDate;
            ExpiredDate = expiredDate;
        }
    }


}
