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
        public void AddPenalty(Guid penaltyId, Decimal amount, String description)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), $"Positive number required, current value: {amount}");
            var existsPenalty = Penalties.FirstOrDefault(_ => _.Id == Id);

            if (existsPenalty == null)
            {
                Penalties.Add(new Penalty(penaltyId, this.Id, amount, description?.Trim()));
                return;
            }

            if (existsPenalty.Amount == amount &&
                existsPenalty.Description == description)
                return;

            throw new PenaltyAlreadyExistsException(existsPenalty);
        }
    }
}
