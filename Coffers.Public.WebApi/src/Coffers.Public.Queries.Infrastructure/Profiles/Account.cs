using System;
using System.Collections.Generic;

namespace Coffers.Public.Queries.Infrastructure.Profiles
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
        /// Список изходящих операций произведенных над счётом
        /// </summary>
        public List<Operation> FromOperations { get; private set; }
    }
}
