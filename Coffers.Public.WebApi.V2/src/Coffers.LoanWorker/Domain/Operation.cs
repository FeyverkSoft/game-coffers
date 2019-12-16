using System;
using Coffers.Types.Account;

namespace Coffers.LoanWorker.Domain
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
        /// Описание операции
        /// </summary>
        public String Description { get; private set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; private set; }

        protected Operation(){}

        public Operation(Guid id, Guid documentId, Decimal amount, OperationType type, String description)
        {
            Id = id;
            DocumentId = documentId;
            Amount = amount;
            Type = type;
            Description = description;
            CreateDate = DateTime.UtcNow;
            OperationDate = DateTime.UtcNow;
        }
    }
}
