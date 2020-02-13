using System;

namespace Coffers.Public.Domain.Operations
{
    public sealed class DocumentNotFoundException : Exception

    {
        public DocumentNotFoundException(String message) : base(message) { }
    }
}
