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
                .Include(g => g.Characters)
                .Include(g => g.DefaultAccount)
                .Include(g => g.Loans)
                .Include(g => g.Penalties)
                .FirstOrDefaultAsync(gamer => gamer.Id == id, cancellationToken);
        }

        public async Task Load(Gamer gamer, CancellationToken cancellationToken)
        {
            await _context.Entry(gamer)
                .Reference(g => g.DefaultAccount)
                .EntityEntry.Collection(g => g.Characters)
                .EntityEntry.Collection(g => g.Loans)
                .EntityEntry.Collection(g => g.Penalties)
                .LoadAsync(cancellationToken);
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
