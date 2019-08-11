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
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; private set; }

        public Guid ConcurrencyTokens { get; private set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; private set; }

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
