using System;

namespace Coffers.Public.Domain.Loans
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// ID гильды
        /// </summary>
        public Guid GuildId { get; }
    }
}
