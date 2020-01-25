using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Penalties;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Penalties
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly PenaltyDbContext _context;

        public UserRepository(PenaltyDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(Guid id, Guid guildId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(_ =>
                    _.Id == id &&
                    _.GuildId == guildId,
                cancellationToken);
        }

        public async Task Save(User user)
        {
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                throw new InvalidOperationException("Detached user entity");

            await _context.SaveChangesAsync();
        }
    }
}
