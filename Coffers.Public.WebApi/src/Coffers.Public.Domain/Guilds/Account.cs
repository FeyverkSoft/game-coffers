using System;

namespace Coffers.Public.Domain.Guilds
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
        public Decimal Balance { get; private set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; private set; }

        public Account()
        {
            Id = Guid.NewGuid();
            Balance = 0;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
