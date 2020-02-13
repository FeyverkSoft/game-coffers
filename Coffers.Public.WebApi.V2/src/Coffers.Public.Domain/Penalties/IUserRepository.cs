using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties.Entity;

namespace Coffers.Public.Domain.Penalties
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, Guid guildId, CancellationToken cancellationToken);

        Task Save(User user);
    }
}
