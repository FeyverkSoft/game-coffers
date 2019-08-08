using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    /// <summary>
    /// Операция над счетами
    /// </summary>
    public sealed class Operation
    {
        /// <summary>
        /// Идентификатор операци
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; private set; }

        /// <summary>
        /// Счёт с которого было списание
        /// </summary>
        public Account FromAccount { get; private set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; private set; }

    }
}
