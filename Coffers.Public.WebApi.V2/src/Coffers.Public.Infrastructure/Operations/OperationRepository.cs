using System;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.Operations;
using Coffers.Public.Domain.Operations.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

using Rabbita.Core;

namespace Coffers.Public.Infrastructure.Operations
{
    public sealed class OperationRepository : IOperationsRepository
    {
        private readonly OperationDbContext _context;
        private readonly IEventBus _bus;
        public OperationRepository(OperationDbContext context,
            IEventBus bus)
        {
            _context = context;
            _bus = bus;
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

            if (operation.Events?.Any() == true)
                foreach (var operationEvent in operation.Events)
                {
                    await _bus.Send(operationEvent);
                }

            await _context.SaveChangesAsync();
        }
    }
}
