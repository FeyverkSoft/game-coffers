﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Guilds
{
    public interface IUserRoleRepository
    {
        Task<Guild> Get(Guid guildId, CancellationToken cancellationToken);
        void Save(Guild guild);
    }
}
