using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Loans
{
    public sealed class OperationRepository : IOperationRepository
    {
        private readonly LoanDbContext _context;

        public OperationRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Operation>> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Operations.AsNoTracking()
                 .Where(_ => _.DocumentId == id)
                 .ToListAsync(cancellationToken);
        }
    }
}
