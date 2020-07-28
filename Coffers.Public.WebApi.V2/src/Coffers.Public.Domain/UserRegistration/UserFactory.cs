using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class UserFactory
    {
        private readonly IUserRepository _repository;
        private readonly IConfirmationCodeProvider _confirmationCodeProvider;

        public UserFactory(IUserRepository repository,
            IConfirmationCodeProvider confirmationCodeProvider)
        {
            _repository = repository;
            _confirmationCodeProvider = confirmationCodeProvider;
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

            return new User(id, guildId, name, rank, status, dateOfBirth, login, null);
        }

        public async Task<User> Registrate(Guid id, Guid guildId, String email, String name, DateTime dateOfBirth, GamerRank rank, GamerStatus status,
            CancellationToken cancellationToken)
        {
            var user = await _repository.Get(id, cancellationToken);

            if (user != null)
            {
                if (user.Email == email &&
                    user.Name == name &&
                    user.DateOfBirth == dateOfBirth)
                    return user;
                throw new UserAlreadyExistsException();
            }

            return new User(id, guildId, name, rank, status, dateOfBirth, null, email);
        }

        public void ResendConfirmationCode(User user)
        {
            var confirmationCode = _confirmationCodeProvider.Generate(user.Email);
            user.ResendConfirmationCode(confirmationCode);
        }
    }
}