using System;
using Coffers.Types.Gamer;

namespace Coffers.LoanWorker.Domain
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
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; private set; }
        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; private set; }

        /// <summary>
        /// Счёт займа
        /// </summary>
        public Account Account { get; private set; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; private set; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; private set; }

        /// <summary>
        /// Статус займа
        /// </summary>

        public LoanStatus LoanStatus { get; private set; }

        /// <summary>
        /// Тариф с которым был взят данный займ
        /// </summary>
        public Tariff Tariff { get; private set; }

        public Guid ConcurrencyTokens { get; private set; }

        protected Loan() { }

        /// <summary>
        /// Прерощает сумму штрафа по займу на указанную величину
        /// </summary>
        /// <param name="penaltyAmount"></param>
        internal void IncrimentPenaltyAmount(Decimal penaltyAmount)
        {
            PenaltyAmount += penaltyAmount;
            Account.ChangeBalance(penaltyAmount);
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = new Guid();
        }

        /// <summary>
        /// Прерощает сумму процентов по займу на указанную величину
        /// </summary>
        /// <param name="penaltyAmount"></param>
        internal void IncrimentTaxAmount(Decimal taxAmount)
        {
            TaxAmount += taxAmount;
            Account.ChangeBalance(taxAmount);
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = new Guid();
        }

        /// <summary>
        /// Устанавливает займу протухший статус
        /// </summary>
        internal void Expire()
        {
            if (LoanStatus == LoanStatus.Expired)
                return;
            if (Account.Balance == 0)
                return;
            LoanStatus = LoanStatus.Expired;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = new Guid();
        }
    }
}
