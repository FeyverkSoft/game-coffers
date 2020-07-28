using System;

namespace Coffers.Public.Domain.Authorization
{
    public interface IPasswordHasher
    {
        Boolean TestPassword(User gamer, String password);

        String GetHash(Guid gamerId, String login, String password);
    }
}
