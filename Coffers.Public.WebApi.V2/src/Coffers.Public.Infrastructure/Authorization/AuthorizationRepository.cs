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

        public async Task<Session> GetSession(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Include(_ => _.User)
                .FirstOrDefaultAsync(session => session.SessionId == id, cancellationToken);
        }
        public async Task SaveSession(Session session)
        {
            var entry = _context.Entry(session);
            if (entry.State == EntityState.Detached)
                _context.Sessions.Add(session);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task<User> GetUser(String login, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(gamer => gamer.Login == login, cancellationToken);
        }

        public async Task<User> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(gamer => gamer.Id == userId, cancellationToken);
        }

        public async Task SaveUser(User gamer)
        {
            var entry = _context.Entry(gamer);
            if (entry.State == EntityState.Detached)
                _context.Users.Add(gamer);

            await _context.SaveChangesAsync();
        }
    }
}
