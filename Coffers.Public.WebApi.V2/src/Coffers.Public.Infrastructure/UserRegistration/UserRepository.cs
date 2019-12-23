using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.UserRegistration;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.UserRegistration
{
    public class UserRepository : IUserRepository
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

        public void Save(User gamer)
        {
            var entry = _context.Entry(gamer);
            if (entry.State == EntityState.Detached)
                _context.Users.Add(gamer);

            _context.SaveChanges();
        }
    }
}
