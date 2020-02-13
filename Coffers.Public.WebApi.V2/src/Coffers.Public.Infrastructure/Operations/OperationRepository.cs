using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations;
using Coffers.Public.Domain.Operations.Entity;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Operations
{
    public sealed class OperationRepository : IOperationsRepository
    {
        private readonly OperationDbContext _context;

        public OperationRepository(OperationDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Operations.FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task Save(Operation operation)
        {
            var entry = _context.Entry(operation);
            if (entry.State == EntityState.Detached)
                _context.Operations.Add(operation);

            await _context.SaveChangesAsync();
        }
    }
}
