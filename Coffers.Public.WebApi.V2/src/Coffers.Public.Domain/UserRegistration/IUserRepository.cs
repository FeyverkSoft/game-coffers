using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.UserRegistration
{
    public interface IUserRegistrationRepository
    {
        Task<User> Get(Guid userId, CancellationToken none);
        Task Save(User gamer, CancellationToken cancellationToken);
        Task<User> GetUserByEmail(String email, Guid guildId, CancellationToken cancellationToken);
    }
}
