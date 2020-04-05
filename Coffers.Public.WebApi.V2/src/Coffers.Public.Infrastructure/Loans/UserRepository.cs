using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Loans
{
    public sealed class GuildRepository : IGuildRepository
    {
        private readonly LoanDbContext _context;

        public GuildRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<Tariff> GetTariff(Guid userId, Guid guildId, CancellationToken cancellationToken)
        {
            return (await _context.Users
                    .AsNoTracking()
                    .Include(_=>_.UserRole)
                    .ThenInclude(_=>_.Tariff)
                    .FirstOrDefaultAsync(_ => _.GuildId == guildId &&
                                              _.Id == userId , cancellationToken))
                ?.UserRole?.Tariff;
        }

        public async Task<Boolean> IsUserExists(Guid userId, Guid guildId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(_ => _.GuildId == guildId &&
                                                              _.Id == userId, cancellationToken);
        }
    }
}
