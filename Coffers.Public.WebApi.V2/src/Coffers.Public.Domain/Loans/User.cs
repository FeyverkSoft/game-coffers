using System;
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
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; }
        public UserRole UserRole { get; }
    }
}
