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
        /// Сумма штрафа
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Уже было выплаченно в пользу штрафа
        /// </summary>
        public Decimal RedemptionAmount { get; set; }
        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; set; }
    }
}
