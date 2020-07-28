using System;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class UserSecurityService
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserSecurityService(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public void CreatePassword(User gamer, String password)
        {
            if (!string.IsNullOrEmpty(gamer.Password))
                throw new InvalidOperationException("У пользователя пароль уже существует.");
            gamer.SetPassword(_passwordHasher.GetHash(gamer.Id, gamer.Login, password));
        }

        public Boolean TestPassword(User gamer, String password)
        {
            return _passwordHasher.TestPassword(gamer, password);
        }
    }
}
