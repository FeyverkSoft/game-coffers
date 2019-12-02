using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Operations
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
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата проведения операции
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Счёт с которого списываются бабки
        /// </summary>
        public Account FromAccount { get; set; }

        public Guid? FromAccountId { get; set; }

        /// <summary>
        /// Счёт на который зачисляются бабки
        /// </summary>
        public Account ToAccount { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; set; }

    }
}
