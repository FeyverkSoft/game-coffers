using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Gamers
{
    public sealed class Penalty
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
        /// Уже было выплаченно в пользу штрафа
        /// </summary>
        public Decimal RepaymentAmount { get; set; }
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
            Amount = amount < 0 ? throw new ArgumentException(nameof(amount)) : amount;
            PenaltyStatus = PenaltyStatus.Active;
            CreateDate = DateTime.UtcNow;
            Description = description;
        }
    }
}
