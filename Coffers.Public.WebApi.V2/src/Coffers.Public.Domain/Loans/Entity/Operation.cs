using System;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Loans.Entity
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

        /// <summary>
        /// Пользователь выполнивший операцию
        /// </summary>
        public Guid UserId { get; }
        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; }

        public DateTime CreateDate { get; } = DateTime.UtcNow;

        protected Operation() { }

        internal Operation(Guid id, Guid userId, Decimal amount, OperationType type, Guid? documentId, Guid guildId)
        {
            Id = id;
            Amount = amount;
            Type = type;
            DocumentId = documentId;
            UserId = userId;
            GuildId = guildId;
        }
    }
}
