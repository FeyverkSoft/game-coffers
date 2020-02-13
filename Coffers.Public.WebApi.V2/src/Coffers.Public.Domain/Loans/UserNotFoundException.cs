using System;

namespace Coffers.Public.Domain.Loans
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid detail)
            : base($"User: {detail} not found")
        {
        }
    }
}
