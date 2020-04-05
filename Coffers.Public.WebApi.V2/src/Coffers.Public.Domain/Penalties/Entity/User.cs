using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Penalties.Entity
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }

        public Guid GuildId { get; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; }

        public List<Penalty> Penalties { get; } = new List<Penalty>();

        public Guid ConcurrencyTokens { get; set; }

        public Penalty AddPenalty(Guid penaltyId, Decimal amount, String description)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), $"Positive number required, current value: {amount}");
            var penalty = Penalties.FirstOrDefault(_ => _.Id == Id);

            if (penalty == null){
                penalty = new Penalty(penaltyId, this.Id, amount, description?.Trim());
                Penalties.Add(penalty);
                return penalty;
            }

            if (penalty.Amount == amount &&
                penalty.Description == description)
                return penalty;

            throw new PenaltyAlreadyExistsException(penalty);
        }
    }
}