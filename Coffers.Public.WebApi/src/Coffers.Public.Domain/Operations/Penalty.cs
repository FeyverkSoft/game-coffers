using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Operations
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
        /// Игрок которому выставили штраф
        /// </summary>
        public Gamer Gamer { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; private set; }

        public Guid ConcurrencyTokens { get; private set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; private set; }

        public Penalty(Guid id, Decimal amount, String description)
        {
            Id = Guid.Empty == id ? throw new ArgumentException(nameof(id)) : id;
            ConcurrencyTokens = Guid.NewGuid();
            PenaltyStatus = PenaltyStatus.Active;
            UpdateDate = DateTime.UtcNow;
            CreateDate = DateTime.UtcNow;
            Description = description;
            Amount = amount < 0? throw new ArgumentException(nameof(amount)) : amount;
        }

        internal void SetStatus(PenaltyStatus canceled)
        {
            if (PenaltyStatus != canceled)
            {
                PenaltyStatus = canceled;
                UpdateDate = DateTime.UtcNow;
                ConcurrencyTokens = Guid.NewGuid();
            }
        }
    }
}
