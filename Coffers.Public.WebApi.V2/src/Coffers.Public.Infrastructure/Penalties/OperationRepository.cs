using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Penalties
{
    public sealed class OperationRepository : IOperationRepository
    {
        private readonly PenaltyDbContext _context;

        public OperationRepository(PenaltyDbContext context)
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
