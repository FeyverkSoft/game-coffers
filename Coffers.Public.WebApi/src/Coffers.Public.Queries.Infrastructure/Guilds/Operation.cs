using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Guilds
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
        /// Дата проведения операции
        /// </summary>
        public DateTime OperationDate { get; private set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; private set; }
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; private set; }

        /// <summary>
        /// Счёт на который зачисляются бабки
        /// </summary>
        public Account ToAccount { get; set; }
    }
}
