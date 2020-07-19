using System;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class NestNotFoundException : Exception
    {
        public NestNotFoundException(Guid id) : base($"Nest {id} not found") { }
    }
}