using System;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.UserRegistration;

using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.UserRegistration
{
    public class UserRepository : IUserRegistrationRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(gamer => gamer.Id == userId, cancellationToken);
        }

        public async Task Save(User gamer, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(gamer);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(gamer, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> GetUserByEmail(String email, Guid guildId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(gamer => gamer.Email == email &&
                                              gamer.GuildId == guildId, cancellationToken);
        }
    }
}
