using System;
using System.Collections.Generic;

namespace Coffers.Public.Domain.Gamers
{
    public sealed class Account
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
            Balance = 0;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
