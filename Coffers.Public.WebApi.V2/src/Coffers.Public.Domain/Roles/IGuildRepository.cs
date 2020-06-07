using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Roles.Entity;

namespace Coffers.Public.Domain.Roles
{
    public interface IGuildRepository
    {
        Task<Guild> Get(Guid guildId, CancellationToken cancellationToken);
        void Save(Guild guild);
    }
}
