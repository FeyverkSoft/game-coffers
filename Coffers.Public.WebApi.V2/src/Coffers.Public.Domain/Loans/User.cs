using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Loans
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
        public List<Loan> Loans { get; } = new List<Loan>();

        public Guid ConcurrencyTokens { get; set; }
    }
}
