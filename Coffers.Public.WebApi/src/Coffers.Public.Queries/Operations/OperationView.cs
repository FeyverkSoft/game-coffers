using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Operations
{
    public sealed class OperationView
    {
        public Guid Id { get; }
        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; }
        /// <summary>
        /// Основание для операции (первичный документ)
        /// </summary>
        public Guid? DocumentId { get; }
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; }
        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; }
        /// <summary>
        /// Дата проведения операции
        /// </summary>
        public DateTime CreateDate { get; }

        public OperationView(
                Guid id,
                Decimal amount,
                Guid? documentId,
                OperationType type,
                String description,
                DateTime createDate
            )
        {
            Id = id;
            Amount = amount;
            DocumentId = documentId;
            Type = type;
            Description = description;
            CreateDate = createDate;
        }
    }
}
