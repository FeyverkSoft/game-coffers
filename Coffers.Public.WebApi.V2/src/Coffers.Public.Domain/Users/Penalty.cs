using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users
{
    public sealed class Penalty
    {

        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; private set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; private set; }

        public Guid ConcurrencyTokens { get; private set; }

        public Penalty(Guid id, Decimal amount, String description)
        {
            Id = Guid.Empty == id ? throw new ArgumentException(nameof(id)) : id;
            PenaltyStatus = PenaltyStatus.Active;
            ConcurrencyTokens = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
            Description = description;
            Amount = amount < 0 ? throw new ArgumentException(nameof(amount)) : amount;
            ConcurrencyTokens = Guid.NewGuid();
        }

        internal void SetStatus(PenaltyStatus canceled)
        {
            if (PenaltyStatus == canceled)
                return;

            PenaltyStatus = canceled;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
