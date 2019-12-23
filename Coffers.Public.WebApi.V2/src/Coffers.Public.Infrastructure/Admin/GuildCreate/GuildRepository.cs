using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Admin.GuildCreate;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Admin.GuildCreate
{
    public class GuildRepository : IGuildRepository
    {
        private readonly GuildsDbContext _context;

        public GuildRepository(GuildsDbContext context)
        {
            _context = context;
        }
        
        public async Task<Domain.Admin.GuildCreate.Guild> Get(Guid id, CancellationToken cancellationToken, Boolean asNonTr = false)
        {
            if (asNonTr)
                return await _context.Guilds
                    .AsNoTracking()
                    .FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);

            return await _context.Guilds
                .FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);
        }

        public async Task Save(Domain.Admin.GuildCreate.Guild guild)
        {
            var entry = _context.Entry(guild);
            if (entry.State == EntityState.Detached)
                _context.Guilds.Add(guild);

            await _context.SaveChangesAsync();
        }
    }
}
