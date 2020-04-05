using System;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Penalty
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Игрок
        /// </summary>
        public User User { get; }
        public Guid UserId { get; }

        /// <summary>
        /// Сумма штрафа
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; }

        public Guid ConcurrencyTokens { get; internal set; }
    }
}
