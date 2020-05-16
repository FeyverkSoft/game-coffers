using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Operations
{
    public sealed class OperationListView
    {
        /// <summary>
        /// Идентификатор платёжной операции
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Сумма операции
        /// Сумма операции может быть не равна сумме документа
        /// </summary>
        public Decimal Amount { get; }

        public DateTime CreateDate { get; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; }

        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid? DocumentId { get; }

        /// <summary>
        /// Сумма документа по которой была создана операция
        /// </summary>
        public Decimal? DocumentAmount { get; }

        /// <summary>
        /// описание документа, если оно есть
        /// </summary>
        public String DocumentDescription { get; }

        /// <summary>
        /// Идентификатор пользоватяля которому пренадлежит операция
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Имя пользователя 
        /// </summary>
        public String UserName { get; }

        public OperationListView ParrentOperation { get; }

        public OperationListView(
            in Guid id,
            in Decimal amount,
            in DateTime date,
            in String description,
            in OperationType type,
            in Guid? documentId,
            in Decimal? documentAmount,
            in String documentDescription,
            in Guid userId,
            in String userName,
            in OperationListView parrentOperation = null)
            => (Id, Amount, CreateDate, Description, Type, DocumentId, DocumentAmount, DocumentDescription, UserId,
                    UserName, ParrentOperation) =
                (id, amount, date, description, type, documentId, documentAmount, documentDescription, userId,
                    userName, parrentOperation);
    }
}