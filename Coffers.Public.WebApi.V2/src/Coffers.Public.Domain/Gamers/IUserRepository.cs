using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Gamers
{
    public interface IUserRepository
    {
        Task<User> Get(Guid userId, CancellationToken none);
        Task Save(User gamer);
        Task Load(User gamer, CancellationToken cancellationToken);
    }
}
