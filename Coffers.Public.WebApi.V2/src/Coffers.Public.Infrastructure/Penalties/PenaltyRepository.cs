using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties;
using Coffers.Public.Domain.Penalties.Entity;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Penalties
{
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly PenaltyDbContext _context;

        public PenaltyRepository(PenaltyDbContext context)
        {
            _context = context;
        }

        public async Task<Penalty> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Penalties
                .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<Penalty> Get(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Penalties
                .FirstOrDefaultAsync(_ =>
                    _.Id == id &&
                    _.UserId == userId, cancellationToken);
        }

        public async Task Save(Penalty penalty)
        {
            var entry = _context.Entry(penalty);
            if (entry.State == EntityState.Detached)
                _context.Penalties.Add(penalty);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Penalty>> GetActivePenalties(CancellationToken cancellationToken)
        {
            return await _context.Penalties.Where(_ => _.PenaltyStatus == PenaltyStatus.Active)
                .ToListAsync(cancellationToken);
        }
    }
}