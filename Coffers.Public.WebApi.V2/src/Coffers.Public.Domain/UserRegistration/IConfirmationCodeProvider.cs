using System;

namespace Coffers.Public.Domain.UserRegistration
{
    public interface IConfirmationCodeProvider
    {
        String Generate(String userEmail);
    }
}
