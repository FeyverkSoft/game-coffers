﻿using System;
using Query.Core;

namespace Coffers.Public.Queries.Users
{
    public sealed class ProfileViewQuery : IQuery<ProfileView>
    {
        /// <summary>
        /// User id
        /// </summary>
        public Guid UserId { get; }
        public Guid GuildId { get; set; }

        public ProfileViewQuery(Guid userId, Guid guildId)
            => (UserId, GuildId)
             = (userId, guildId);
    }
}
