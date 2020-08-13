using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans.Entity;

namespace Coffers.Public.Domain.Loans
{
    public interface IOperationRepository
    {
        public Task<ICollection<Operation>> GetByDocument(Guid id, CancellationToken cancellationToken);
    }
}
