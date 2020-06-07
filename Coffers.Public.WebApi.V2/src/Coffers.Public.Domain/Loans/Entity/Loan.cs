using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Helpers;
using Coffers.Types.Account;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Loans.Entity
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
        public Tariff Tariff { get; }

        public IList<Operation> Operations { get; } = new List<Operation>();

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
        public Decimal TaxAmount { get; private set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; private set; } = 0;

        public LoanStatus LoanStatus { get; private set; } = LoanStatus.Active;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

        public Boolean IsActive => LoanStatus == LoanStatus.Active ||
                                   LoanStatus == LoanStatus.Expired;

        /// <summary>
        /// Займ стух?
        /// </summary>
        public Boolean IsExpired => ExpiredDate < DateTime.UtcNow ||
                                    LoanStatus == LoanStatus.Expired;

        /// <summary>
        /// Займ без налога?
        /// </summary>
        /// <returns></returns>
        internal Boolean IsFreeTax => Tariff == null || Tariff.LoanTax <= 0;

        /// <summary>
        /// Время существования займа
        /// </summary>
        internal Int32 Lifetime =>
            (Int32) Math.Floor((DateTime.UtcNow.Trunc(DateTruncType.Day) - (IsActive ? CreateDate : UpdateDate).Trunc(DateTruncType.Day)).TotalDays);

        /// <summary>
        /// Время просрочки в днях
        /// </summary>
        internal Int32 ExpireLifetime =>
            (Int32) Math.Floor((DateTime.UtcNow.Trunc(DateTruncType.Day) - ExpiredDate.Trunc(DateTruncType.Day))
                .TotalDays);


        protected Loan() { }

        internal Loan(Guid id, Guid userId, Guid? tariffId, Guid guildId, string description, DateTime expiredDate, decimal amount, decimal taxAmount)
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
            Operations.Add(new Operation(id, userId, -1 * amount, OperationType.Loan, id, guildId));
        }

        public void MakeCancel()
        {
            if (LoanStatus == LoanStatus.Canceled)
                return;

            if (!IsActive)
                throw new InvalidOperationException($"Incorrect current loan state; State:{LoanStatus}; Id:{Id}");

            if (Operations.Count > 1)
                throw new InvalidOperationException($"Incorrect loan; There should not be any loan transactions.");

            var reverseOperations = Operations.Select(operation =>
                new Operation(Guid.NewGuid(), operation.UserId, -1 * operation.Amount, OperationType.Loan, operation.DocumentId, operation.GuildId)).ToList();

            foreach (var reverseOperation in reverseOperations){
                Operations.Add(reverseOperation);
            }

            LoanStatus = LoanStatus.Canceled;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }


        internal void MakePaid()
        {
            if (!IsActive)
                throw new InvalidOperationException($"Incorrect current loan state; State:{LoanStatus}; Id:{Id}");

            LoanStatus = LoanStatus.Paid;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }

        internal void SetPenaltyAmount(Decimal penaltyAmount)
        {
            if (penaltyAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(penaltyAmount), "Non-negative number required");

            PenaltyAmount = penaltyAmount;
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        internal void SetTaxAmount(Decimal taxAmount)
        {
            if (taxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(taxAmount), "Non-negative number required");

            TaxAmount = taxAmount;
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        public void MakeExpired()
        {
            if (LoanStatus == LoanStatus.Expired)
                return;

            if (LoanStatus == LoanStatus.Paid ||
                LoanStatus == LoanStatus.Canceled)
                throw new InvalidOperationException($"Incorrect current loan state; State:{LoanStatus}; Id:{Id}");

            LoanStatus = LoanStatus.Expired;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }

        /// <summary>
        /// Сума налога в день
        /// </summary>
        /// <returns></returns>
        internal Decimal GetTaxAmountPerDay() => IsFreeTax ? 0 : Amount * (Tariff.ExpiredLoanTax / 100);

        /// <summary>
        /// Сумма штрафа за просрочку в день
        /// </summary>
        /// <returns></returns>
        public Decimal GetExpireTaxAmountPerDay() => IsFreeTax ? 0 : Amount * (Tariff.ExpiredLoanTax / 100);
    }
}