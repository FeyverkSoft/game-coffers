using System;
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


        public async Task<Guild> Get(Guid id, CancellationToken cancellationToken, Boolean asNonTr = false)
        {
            if (asNonTr)
                return await _context.Guilds.Include(x => x.Gamers)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);

            return await _context.Guilds.Include(x => x.Gamers)
                .FirstOrDefaultAsync(guild => guild.Id == id, cancellationToken);
        }

        /// <summary>
        /// Подгружает список игроков в объект гильдии
        /// </summary>
        /// <param name="guild"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task LoadGamers(Guild guild, CancellationToken cancellationToken)
        {
            await _context.Entry(guild)
                .Collection(e => e.Gamers)
                .LoadAsync(cancellationToken);
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
