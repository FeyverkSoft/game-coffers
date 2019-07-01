using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Guilds;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Guilds
{
    public class GuildsQueryHandler : IQueryHandler<GuildQuery, GuildView>
    {
        private readonly GuildsDbContext _context;

        public GuildsQueryHandler(GuildsDbContext context)
        {
            _context = context;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            var res =  await _context.Guilds
                .FirstOrDefaultAsync(client => client.Id == query.Id, cancellationToken);
            return new GuildView
            {
                Id = res.Id,
                IsActive = res.IsActive,
                UpdateDate = res.UpdateDate,
                CreateDate = res.CreateDate
            };
        }

    }
}
