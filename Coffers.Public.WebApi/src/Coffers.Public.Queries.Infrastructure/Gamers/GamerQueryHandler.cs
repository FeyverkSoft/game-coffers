using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Gamers;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Queries.Infrastructure.Gamers
{
    public sealed class GamerQueryHandler :
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>,
        IQueryHandler<GetGamerInfoQuery, GamerInfoView>

    {
        private readonly GamerQueryDbContext _context;
        public GamerQueryHandler(GamerQueryDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GamersListView>> Handle(GetGamersQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Gamers
                .AsNoTracking()
                .Where(g => g.GuildId == query.GuildId);

            var dateFrom = (query.DateFrom ?? DateTime.UtcNow).Trunc(DateTruncType.Month);
            var dateTo = (query.DateTo ?? DateTime.UtcNow.AddMonths(1)).Trunc(DateTruncType.Month);

            if (query.GamerStatuses != null)
                q = q.Where(g => query.GamerStatuses.Contains(g.Status));

            q = q.Where(g => g.DeletedDate == null || g.DeletedDate >= dateFrom);

#warning прячим служебного временного пользователя
            q = q.Where(g => g.Name != "user");

            if (dateTo != null)
                q = q.Where(g => g.CreateDate <= dateTo);

            return (await q.Select(g => new GamersListView
            (
                g.Id,
                g.Name,
                g.DefaultAccount.Balance,
                g.Characters
                    .Where(c => c.Status == CharStatus.Active)
                    .OrderBy(c => c.Name)
                    .Select(x => new CharacterView(x.Name, x.ClassName))
                    .ToList(),
                g.Rank,
                g.Status,
                g.DateOfBirth.Trunc(DateTruncType.Day),
                g.Penalties
                    .Where(p => p.CreateDate >= dateFrom || p.PenaltyStatus == PenaltyStatus.Active)
                    .OrderBy(_ => _.CreateDate)
                    .Select(p => new PenaltyView
                    (
                        p.Id,
                        p.Amount,
                        p.CreateDate,
                        p.Description,
                        p.PenaltyStatus
                    )).ToList(),
                 g.Loans
                     .Where(l => l.CreateDate >= dateFrom || l.LoanStatus == LoanStatus.Active || l.LoanStatus == LoanStatus.Expired || l.ExpiredDate >= dateFrom)
                     .OrderBy(_=>_.CreateDate)
                     .Select(l => new LoanView
                     (
                         l.Id,
                         l.Amount,
                         l.Account.Balance,
                         l.CreateDate,
                         l.Description,
                         l.LoanStatus,
                         l.ExpiredDate
                     )).ToList()
            ))
            .ToListAsync(cancellationToken))
            .OrderBy(_ => _.Status)
                .ThenBy(_ => _.Rank)
                .ThenBy(_ => _.DateOfBirth)
            .ToList();
        }

        public async Task<GamerInfoView> Handle(GetGamerInfoQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Gamers
                .AsNoTracking()
                .Include(_ => _.DefaultAccount)
                .Where(g => g.Id == query.UserId);

            return await q
                .Select(_ => new GamerInfoView(
                    _.Id,
                    _.DefaultAccount.Id,
                    _.GuildId
                ))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
