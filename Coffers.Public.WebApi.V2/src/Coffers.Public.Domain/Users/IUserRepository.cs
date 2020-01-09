using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> Get(Guid userId, Guid guildId, CancellationToken none);
        void Save(User gamer);
    }
}
