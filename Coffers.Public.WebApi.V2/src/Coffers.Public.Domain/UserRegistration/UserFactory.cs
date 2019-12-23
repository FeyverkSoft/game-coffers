using System;
using System.Threading.Tasks;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class UserFactory
    {
        public async Task<User> Create(Guid id, Guid guildId, string login, string name, DateTime dateOfBirth, GamerRank rank, GamerStatus status)
        {
            return new User(id, guildId, name, rank, status, dateOfBirth, login);
        }
    }
}
