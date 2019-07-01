using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Guilds
{
    public class GuildRepository : IGuildRepository
    {
        private readonly GuildsDbContext _context;

        public GuildRepository(GuildsDbContext context)
        {
            _context = context;
        }

        public async Task<Guild> Get(string id, CancellationToken cancellationToken)
        {
            return await _context.Guilds.FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);
        }

        public async Task Save(Guild guild)
        {
            var entry = _context.Entry(guild);
            if (entry.State == EntityState.Detached)
                _context.Guilds.Add(guild);

            await _context.SaveChangesAsync();
        }
    }
}
