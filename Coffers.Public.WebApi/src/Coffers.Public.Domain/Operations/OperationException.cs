using System;

namespace Coffers.Public.Domain.Operations
{
    public class OperationException : Exception
    {
        public OperationException(String message, Exception inner = null) : base(message, inner) { }
    }
}
