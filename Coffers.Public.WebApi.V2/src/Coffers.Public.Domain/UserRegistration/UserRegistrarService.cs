using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class UserRegistrarService
    {
        private readonly IUserRegistrationRepository _repository;
        private readonly IConfirmationCodeProvider _confirmationCodeProvider;
        private readonly IPasswordHasher _passwordHasher;

        public UserRegistrarService(
            IUserRegistrationRepository repository,
            IConfirmationCodeProvider confirmationCodeProvider,
            IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _confirmationCodeProvider = confirmationCodeProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Create(Guid id, Guid guildId, String login, String name, DateTime dateOfBirth, GamerRank rank, GamerStatus status,
            CancellationToken cancellationToken)
        {
            var user = await _repository.Get(id, cancellationToken);

            if (user != null){
                if (user.Login == login &&
                    user.Name == name &&
                    user.DateOfBirth == dateOfBirth)
                    return user;
                throw new UserAlreadyExistsException();
            }

            return new User(id, guildId, name, rank, status, dateOfBirth, login, null);
        }

        public async Task<User> CreateByEmail(Guid id, Guid guildId, String email, String password, String name, DateTime? dateOfBirth = null,
            GamerRank rank = GamerRank.Beginner, GamerStatus status = GamerStatus.New,
            CancellationToken cancellationToken = default)
        {
            var user = await _repository.Get(id, cancellationToken);
            
            if (user == null)
                user = await _repository.GetUserByEmail(email, guildId, cancellationToken);
            
            if (user != null){
                if (user.Id == id &&
                    user.Email == email &&
                    user.Name == name &&
                    user.GuildId == guildId &&
                    (dateOfBirth == null || user.DateOfBirth == dateOfBirth))
                    return user;
                throw new UserAlreadyExistsException();
            }

            user = new User(id, guildId, name, rank, status, dateOfBirth ?? DateTime.UtcNow, null, email);
            _passwordHasher.GetHash(user.Id, user.Email, password);
            return user;
        }

        public void ResendConfirmationCode(User user)
        {
            var confirmationCode = _confirmationCodeProvider.Generate(user.Email, user.Id);
            user.ResendConfirmationCode(confirmationCode);
        }

        public async Task<User> Confirm(String confirmationCode, CancellationToken cancellationToken)
        {
            if (!_confirmationCodeProvider.Validate(confirmationCode, out var email, out var userId))
                throw new InvalidTokenException();

            var user = await _repository.Get(userId, cancellationToken);

            if (email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)){
                user.Confirm();
                return user;
            }

            throw new InvalidTokenException();
        }
    }
}