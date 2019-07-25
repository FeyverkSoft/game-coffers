using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Operations
{
    public sealed class OperationView
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Основание для операции (первичный документ)
        /// </summary>
        public Guid? DocumentId { get; set; }
        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }
        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Дата проведения операции
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
