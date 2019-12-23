using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.UserRegistration
{
    public interface IUserRepository
    {
        Task<User> Get(Guid userId, CancellationToken none);
        void Save(User gamer);
    }
}
