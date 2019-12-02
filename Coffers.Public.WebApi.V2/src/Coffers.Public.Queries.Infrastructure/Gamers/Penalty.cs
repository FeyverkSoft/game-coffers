using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Gamers
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
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; private set; }

        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; private set; }
    }
}
