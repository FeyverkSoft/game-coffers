using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.NestContracts;
using Coffers.Types.Nest;

using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.NestContracts
{
    public sealed class NestContractRepository :
        INestContractRepository,
        INestGetter
    {
        private readonly NestContractDbContext _context;

        public NestContractRepository(NestContractDbContext context)
        {
            _context = context;
        }

        async Task<NestContract?> INestContractRepository.Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.NestContracts.SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        async Task INestContractRepository.Save(NestContract loan, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(loan);
            if (entry.State == EntityState.Detached)
                await _context.NestContracts.AddAsync(loan, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Int32> GetActiveCount(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.NestContracts.CountAsync(_ => _.UserId == userId &&
                                                                _.Status == NestContractStatus.Active,
                cancellationToken);
        }

        public async Task<IEnumerable<NestContract>> GetAllUnprocessedExpired(CancellationToken cancellationToken)
        {
            return await _context.NestContracts.Where(_ => _.ExpDate != null &&
                                                           _.ExpDate <= DateTime.UtcNow &&
                                                           _.Status == NestContractStatus.Active)
                .ToListAsync(cancellationToken);
        }

        async Task<Nest?> INestGetter.Get(Guid nestId, Guid guildId, CancellationToken cancellationToken)
        {
            return await _context.Nests.SingleOrDefaultAsync(_ => _.GuildId == guildId &&
                                                                  _.Id == nestId &&
                                                                  !_.IsHidden, cancellationToken);
        }
    }
}