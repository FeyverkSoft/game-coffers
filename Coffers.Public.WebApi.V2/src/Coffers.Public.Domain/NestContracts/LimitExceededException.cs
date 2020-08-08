using System;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class LimitExceededException : Exception
    {
        public LimitExceededException(Int32 count) :base($"The limit of {count} contracts has been exceeded."){ }
    }
}
