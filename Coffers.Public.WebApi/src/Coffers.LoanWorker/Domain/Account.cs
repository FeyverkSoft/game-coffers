using System;

namespace Coffers.LoanWorker.Domain
{
    public sealed class Account
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public Decimal Balance { get; private set; } = 0;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных конкурентных запросах
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; }

        protected Account() { }

        public void ChangeBalance(Decimal amount)
        {
            Balance += amount;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
