using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Authorization
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly AuthorizationDbContext _context;

        public AuthorizationRepository(AuthorizationDbContext context)
        {
            _context = context;
        }

        public async Task<Session> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Include(_ => _.Gamer)
                .FirstOrDefaultAsync(session => session.SessionId == id, cancellationToken);
        }
        public async Task Save(Session session)
        {
            var entry = _context.Entry(session);
            if (entry.State == EntityState.Detached)
                _context.Sessions.Add(session);

            await _context.SaveChangesAsync();
        }

        public async Task<Gamer> FindGamer(String login, CancellationToken cancellationToken)
        {
            return await _context.Gamers
                .FirstOrDefaultAsync(gamer => gamer.Login == login, cancellationToken);
        }

        public async Task<Gamer> GetGamer(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Gamers
                .FirstOrDefaultAsync(gamer => gamer.Id == userId, cancellationToken);
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
