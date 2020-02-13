using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties.Entity;

namespace Coffers.Public.Domain.Penalties
{
    public interface IOperationRepository
    {
        public Task<ICollection<Operation>> Get(Guid id, CancellationToken cancellationToken);
    }
}
