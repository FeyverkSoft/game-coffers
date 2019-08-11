using System;

namespace Coffers.Public.Domain.Operations
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
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; }


        public void ChangeBalance(Decimal amount)
        {
            Balance += amount;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
