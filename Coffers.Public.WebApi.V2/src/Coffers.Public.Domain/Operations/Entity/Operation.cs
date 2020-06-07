using System;
using System.Collections.Generic;
using Coffers.Public.Domain.Operations.Events;
using Coffers.Types.Account;
using Rabbita.Core;

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

        public Operation(Guid id, Guid guildId, Guid userId, Decimal amount, Guid? parentOperationId, String description)
            => (Id, GuildId, UserId, Amount, ParentOperationId, Description)
                = (id, guildId, userId, amount, parentOperationId, description);

        internal void SetDocument(OperationType type, Guid documentId)
        {
            if (Type == type && DocumentId == documentId)
                return;
            if (Type == type && DocumentId != documentId)
                throw new InvalidOperationException($"The document is already set. Current document: «{documentId}».");

            Type = type;
            DocumentId = documentId;

            switch (type){
                case OperationType.Penalty:
                    Events.Add(new PenaltyOperationCreated(Id, documentId));
                    break;
                case OperationType.Loan:
                    Events.Add(new LoanOperationCreated(Id, documentId));
                    break;
                case OperationType.Tax:
                case OperationType.Sell:
                case OperationType.Emission:
                case OperationType.Output:
                case OperationType.Other:
                case OperationType.Deal:
                case OperationType.LoanTax:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal void SetOperationWithoutDocument(OperationType type)
        {
            if (Type == type)
                return;
            if (DocumentId != null)
                throw new InvalidOperationException("У данной операции уже есть документ. Изменение такой операции невозможно");
            Type = type;
        }
    }
}