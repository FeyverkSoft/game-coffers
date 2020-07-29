using System;
using System.Security.Cryptography;
using System.Text;

using Coffers.Public.Domain.Authorization;

namespace Coffers.Public.Infrastructure.Authorization
{
    public sealed class PasswordHasher :
       Domain.Authorization.IPasswordHasher,
       Domain.UserRegistration.IPasswordHasher
    {
        public Boolean TestPassword(User gamer, String password)
        {
            return GetHash(gamer.Id, gamer.Login ?? gamer.Email, password)
                .Equals(gamer.Password, StringComparison.InvariantCultureIgnoreCase);
        }

        public String GetHash(Guid gamerId, String login, String password)
        {
            using var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(login + password + gamerId));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
