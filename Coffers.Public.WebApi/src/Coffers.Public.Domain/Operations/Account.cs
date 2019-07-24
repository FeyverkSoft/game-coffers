using System;
using System.Collections.Generic;

namespace Coffers.Public.Domain.Operations
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
        public Decimal Balance { get; private set; } = 0;

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }

        /// <summary>
        /// Список изходящих операций произведенных над счётом
        /// </summary>
        public List<Operation> FromOperations { get; set; }

        /// <summary>
        /// Список входящих операций произведенных над счётом
        /// </summary>
        public List<Operation> ToOperations { get; set; }

        public void ChangeBalance(Decimal amount)
        {
            Balance += amount;
            ConcurrencyTokens = Guid.NewGuid();
        }

        public Account()
        {
            Id = Guid.NewGuid();
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
