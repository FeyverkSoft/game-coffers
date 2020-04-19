using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Penalties.Entity
{
    public sealed class Penalty
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; }

        public Guid UserId { get; }
        public User User { get; }

        /// <summary>
        /// Сумма штрафа
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; private set; } = PenaltyStatus.Active;

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; }

        public Guid ConcurrencyTokens { get; internal set; } = Guid.NewGuid();
        public Boolean IsActive => PenaltyStatus == PenaltyStatus.Active;
        
        public Penalty(Guid id, Guid userId, Decimal amount, String description)
        => (Id, UserId, Amount, Description)
        = (id, userId, amount < 0 ? throw new ArgumentOutOfRangeException(nameof(amount), "Non-negative number required") : amount, description?.Trim());

        public void MakeCancel()
        {
            if (PenaltyStatus == PenaltyStatus.Canceled)
                return;

            if (PenaltyStatus == PenaltyStatus.InActive)
                throw new InvalidOperationException($"Invalid current penlty state; Current: {PenaltyStatus}; Id: {Id}");

            PenaltyStatus = PenaltyStatus.Canceled;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }

        internal void MakeInActive()
        {
            if (!IsActive)
                throw new InvalidOperationException($"Invalid current penlty state; Current: {PenaltyStatus}; Id: {Id}");

            PenaltyStatus = PenaltyStatus.InActive;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
