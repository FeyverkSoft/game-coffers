using System;

namespace Coffers.Public.Domain.UserRegistration
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base()
        {
        }
    }
}