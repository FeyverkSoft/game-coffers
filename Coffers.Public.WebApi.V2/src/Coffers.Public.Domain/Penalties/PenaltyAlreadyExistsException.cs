using System;

namespace Coffers.Public.Domain.Penalties
{
    public sealed class PenaltyAlreadyExistsException : Exception
    {
        public Penalty Detail { get; }
        public PenaltyAlreadyExistsException(Penalty detail)
            : base("Penalty already exists")
        {
            Detail = detail;
        }
    }
}
