using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Users
{
    public sealed class CharacterViewQuery : IQuery<IEnumerable<CharacterView>>
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; }
        public Guid GuildId { get; set; }

        public CharacterViewQuery(Guid userId, Guid guildId)
            => (UserId, GuildId)
             = (userId, guildId);
    }
}
