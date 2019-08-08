using System;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Penalty
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Игрок
        /// </summary>
        public Gamer Gamer { get; set; }

        /// <summary>
        /// Сумма штрафа
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; set; }

        public Guid ConcurrencyTokens { get; internal set; }
    }
}
