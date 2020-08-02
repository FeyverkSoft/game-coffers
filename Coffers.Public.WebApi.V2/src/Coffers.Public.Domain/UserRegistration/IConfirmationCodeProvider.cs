using System;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.UserRegistration
{
    public interface IConfirmationCodeProvider
    {
        String Generate(String userEmail, Guid userId);
        Boolean Validate(in String token, out String userEmail, out Guid userId);
    }
}
