using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Penalties
{
    public sealed class Penalty
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; }

        public Guid UserId { get; }

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
        public DateTime UpdateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; } = PenaltyStatus.Active;

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; }

        public Guid ConcurrencyTokens { get; internal set; } = Guid.NewGuid();
        public Penalty(Guid id, Guid userId, Decimal amount, String description)
        => (Id, UserId, Amount, Description)
        = (id, userId, amount < 0 ? throw new ArgumentOutOfRangeException(nameof(amount), "Non-negative number required") : amount, description?.Trim());
    }
}
