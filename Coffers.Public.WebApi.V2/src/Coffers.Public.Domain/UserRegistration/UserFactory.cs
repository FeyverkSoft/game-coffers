using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class UserFactory
    {
        private readonly IUserRepository _repository;

        public UserFactory(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Create(Guid id, Guid guildId, string login, string name, DateTime dateOfBirth, GamerRank rank, GamerStatus status,
            CancellationToken cancellationToken)
        {
            var user = await _repository.Get(id, cancellationToken);

            if (user != null)
            {
                if (user.Login == login &&
                    user.Name == name &&
                    user.DateOfBirth == dateOfBirth)
                    return user;
                throw new UserAlreadyExistsException();
            }

            return new User(id, guildId, name, rank, status, dateOfBirth, login);
        }
    }
}