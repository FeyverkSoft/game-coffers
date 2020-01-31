using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public interface IOperationRepository
    {
        public Task<ICollection<Operation>> Get(Guid id, CancellationToken cancellationToken);
    }
}
