using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Gamers;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Gamers
{
    public class GamerRepository : IGamerRepository
    {
        private readonly GamerDbContext _context;

        public GamerRepository(GamerDbContext context)
        {
            _context = context;
        }

        public async Task<Gamer> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Gamers
                .FirstOrDefaultAsync(gamer => gamer.Id == id, cancellationToken);
        }
        public async Task Save(Gamer gamer)
        {
            var entry = _context.Entry(gamer);
            if (entry.State == EntityState.Detached)
                _context.Gamers.Add(gamer);

            await _context.SaveChangesAsync();
        }
    }
}
