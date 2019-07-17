using System;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Guilds
{
    /// <summary>
    /// Операция над счетами
    /// </summary>
    public sealed class Operation
    {
        /// <summary>
        /// Идентификатор операци
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата проведения операции
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }
    }
}
