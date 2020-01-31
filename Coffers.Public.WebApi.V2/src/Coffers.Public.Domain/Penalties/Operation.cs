using System;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Penalties
{
    /// <summary>
    /// Операция над счетами
    /// </summary>
    public sealed class Operation
    {
        /// <summary>
        /// Идентификатор операци
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; }
    }
}
