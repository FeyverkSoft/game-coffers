using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Roles
{
    public class GuildRepository : IGuildRepository
    {
        private readonly GuildsDbContext _context;

        public GuildRepository(GuildsDbContext context)
        {
            _context = context;
        }

        public async Task<Guild> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context
                .Guilds
                .Include(_ => _.Roles)
                .FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);
        }

        public void Save(Guild guild)
        {
            var entry = _context.Entry(guild);
            if (entry.State == EntityState.Detached)
                _context.Guilds.Add(guild);

            _context.SaveChanges();
        }
    }
}
