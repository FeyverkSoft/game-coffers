using System;
using System.Collections.Generic;
using Coffers.Public.Domain.Operations.Events;
using Coffers.Types.Account;
using Core.Rabbita;

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
        public DateTime CreateDate { get; } = DateTime.UtcNow;

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

        public List<IEvent> Events { get; } = new List<IEvent>();

        protected Operation() { }

        public Operation(Guid id, Guid guildId, Guid userId, Decimal amount, Guid? documentId, OperationType type, Guid? parentOperationId, String description)
        => (Id, GuildId, UserId, Amount, DocumentId, Type, ParentOperationId, Description)
            = (id, guildId, userId, amount, documentId, type, parentOperationId, description);

        internal void SetDocument(OperationType type, Guid documentId)
        {
            if (Type == type && DocumentId == documentId)
                return;
            if (Type == type && DocumentId != documentId)
                throw new InvalidOperationException($"The document is already set. Current document: «{documentId}».");

            Type = type;
            DocumentId = documentId;

            switch (type)
            {
                case OperationType.Tax:
                    break;
                case OperationType.Sell:
                    break;
                case OperationType.Penalty:
                    Events.Add(new PenaltyOperationCreated(Id, documentId));
                    break;
                case OperationType.Loan:
                    Events.Add(new LoanOperationCreated(Id, documentId));
                    break;
                case OperationType.Emission:
                    break;
                case OperationType.Output:
                    break;
                case OperationType.Other:
                    break;
                case OperationType.Deal:
                    break;
                case OperationType.LoanTax:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
