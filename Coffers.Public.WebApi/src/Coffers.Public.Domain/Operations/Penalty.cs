using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Operations
{
    public sealed class Penalty
    {

        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; internal set; }

        /// <summary>
        /// Игрок которому выставили штраф
        /// </summary>
        public Gamer Gamer { get; internal set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; set; }

        public Penalty(Guid id, Decimal amount, String description)
        {
            Id = Guid.Empty == id ? throw new ArgumentException(nameof(id)) : id;
            PenaltyStatus = PenaltyStatus.Active;
            CreateDate = DateTime.UtcNow;
            Description = description;
            Amount = amount < 0? throw new ArgumentException(nameof(amount)) : amount;
        }
    }
}
