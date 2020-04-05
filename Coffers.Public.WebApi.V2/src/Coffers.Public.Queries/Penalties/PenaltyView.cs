using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Penalties
{
    public sealed class PenaltyView
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime CreateDate { get; }
        /// <summary>
        /// Сумма
        /// </summary>
        public Decimal Amount { get; }
        /// <summary>
        /// Причина
        /// </summary>
        public String Description { get; }
        /// <summary>
        /// Статус штрафа
        /// </summary>
        public PenaltyStatus PenaltyStatus { get; }
        /// <summary>
        /// Penalty unique id
        /// </summary>
        public Guid Id { get; }

        public PenaltyView(Guid id, decimal amount, DateTime createDate, string description, PenaltyStatus penaltyStatus) 
            => (Id, Amount, CreateDate, Description, PenaltyStatus)
            =  (id, amount, createDate, description, penaltyStatus);
    }
}