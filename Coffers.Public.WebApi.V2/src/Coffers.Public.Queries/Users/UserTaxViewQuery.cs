using System;
using Query.Core;

namespace Coffers.Public.Queries.Users
{
    public sealed class UserTaxViewQuery : IQuery<UserTaxView>
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; }
        public Guid GuildId { get; set; }

        public UserTaxViewQuery(Guid userId, Guid guildId)
            => (UserId, GuildId)
                = (userId, guildId);
    }
}
