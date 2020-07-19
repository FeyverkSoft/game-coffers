using System;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class ContractAlreadyExistsException : Exception
    {
        public ContractAlreadyExistsException(Guid id) : base($"Contract {id} already exists") { }
    }
}