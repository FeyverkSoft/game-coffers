using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class PenaltyView
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public Decimal Amount { get; set; }
        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; set; }  
        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; set; }
        /// <summary>
        /// Penalty unique id
        /// </summary>
        public Guid Id { get; set; }
    }
}
