using System;
using System.Security.Cryptography;
using System.Text;

namespace Coffers.Public.Domain.Authorization
{
    public sealed class GamerSecurityService
    {
        public void CreatePassword(Gamer gamer, String password)
        {
            if (!string.IsNullOrEmpty(gamer.Password))
                throw new InvalidOperationException("У пользователя пароль уже существует.");
            gamer.SetPassword(GetHash(gamer.Id, gamer.Login, password));
        }

        public Boolean TestPassword(Gamer gamer, String password)
        {
            return GetHash(gamer.Id, gamer.Login, password).Equals(gamer.Password, StringComparison.InvariantCultureIgnoreCase);
        }

        private String GetHash(Guid gamerId, String login, String password)
        {
            var crypt = new SHA256Managed();
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
