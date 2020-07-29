using System;

namespace Coffers.Public.Domain.UserRegistration
{
    public interface IPasswordHasher
    {
        String GetHash(Guid gamerId, String login, String password);
    }
}
