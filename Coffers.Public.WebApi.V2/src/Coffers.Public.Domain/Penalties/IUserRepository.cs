using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Penalties
{
    public interface IUserRepository
    {
        Task<User> Get(Guid id, Guid guildId, CancellationToken cancellationToken);

        void Save(User user);
    }
}
