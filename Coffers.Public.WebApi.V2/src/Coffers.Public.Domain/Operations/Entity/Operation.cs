using System;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations.Entity
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
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; }
        /// <summary>
        /// Пользователь выполнивший операцию
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Ссылка на родительскую проводку
        /// </summary>
        public Guid? ParentOperationId { get; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; private set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; private set; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; }

        protected Operation() { }

        public Operation(Guid id, Guid guildId, Guid userId, Decimal amount, Guid? documentId, OperationType type, Guid? parentOperationId, String description)
        => (Id, GuildId, UserId, Amount, DocumentId, Type, ParentOperationId, Description)
            = (id, guildId, userId, amount, documentId, type, parentOperationId, description);

        internal void SetDocument(OperationType type, Guid documentId)
        {
            Type = type;
            DocumentId = documentId;
        }
    }
}
