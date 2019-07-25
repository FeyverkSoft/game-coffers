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

namespace Coffers.Public.Infrastructure.Gamers
{
    public sealed class GamerQueryHandler : IQueryHandler<GetBaseGamerInfoQuery, BaseGamerInfoView>,
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>
    {
        private readonly GamerDbContext _context;

        public GamerQueryHandler(GamerDbContext context)
        {
            _context = context;
        }

        public async Task<BaseGamerInfoView> Handle(GetBaseGamerInfoQuery query, CancellationToken cancellationToken)
        {
            return await _context.Gamers
                .Where(g => g.Id == query.UserId)
                .AsNoTracking()
                .Select(g => new BaseGamerInfoView
                {
                    UserId = g.Id,
                    Balance = g.DefaultAccount.Balance,
                    Name = g.Name,
                    Rank = g.Rank,
                    CharCount = g.Characters.Count(c => c.Status == CharStatus.Active),
                    ActiveLoanAmount = g.Loans.Where(l => l.LoanStatus == LoanStatus.Active)
                        .Sum(l => (l.Amount)),
                    ActiveExpLoanAmount = g.Loans.Where(l => l.LoanStatus == LoanStatus.Active)
                        .Sum(l => l.PenaltyAmount),
                    ActiveLoanTaxAmount = g.Loans.Where(l => l.LoanStatus == LoanStatus.Active)
                        .Sum(l => l.TaxAmount),
                    RepaymentLoanAmount = g.Loans.Where(l => l.LoanStatus == LoanStatus.Active)
                        .Sum(l => l.TaxAmount),
                    ActivePenaltyAmount = g.Penalties.Where(p => p.PenaltyStatus == PenaltyStatus.Active)
                        .Sum(p => p.Amount),
                })
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<ICollection<GamersListView>> Handle(GetGamersQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Gamers
                .AsNoTracking()
                .Where(g => g.GuildId == query.GuildId);

            var dateFrom = (query.DateFrom ?? DateTime.UtcNow).Trunc(DateTruncType.Month);

            if (query.GamerStatuses != null)
                q = q.Where(g => query.GamerStatuses.Contains(g.Status));

            q = q.Where(g => g.DeletedDate == null || g.DeletedDate >= dateFrom);

            if (query.DateTo != null)
                q = q.Where(g => g.CreateDate <= query.DateTo.Value.Trunc(DateTruncType.Month));

            q = q.Include(g => g.DefaultAccount)
             .Include(g => g.Characters)
             .Include(g => g.Loans)
             .Include(g => g.Penalties);
            return await q.Select(g => new GamersListView
            {
                Id = g.Id,
                Balance = g.DefaultAccount.Balance,
                Characters =  g.Characters.Where(c => c.Status == CharStatus.Active).Select(x => x.Name).ToList(),
                Rank = g.Rank,
                Status = g.Status,
                Penalties = g.Penalties.Where(p => p.CreateDate >= dateFrom)
                    .Select(p => new PenaltyView
                    {
                        Id = p.Id,
                        Amount = p.Amount,
                        Date = p.CreateDate,
                        Description = p.Description,
                        PenaltyStatus = p.PenaltyStatus
                    }).ToList(),
                Loans = g.Loans.Where(l => l.CreateDate >= dateFrom)
                    .Select(l => new LoanView
                    {
                        Amount = l.Amount,
                        Date = l.CreateDate,
                        Description = l.Description,
                        LoanStatus = l.LoanStatus,
                        ExpiredDate = l.ExpiredDate,
                        Id = l.Id
                    }).ToList(),
            })
                .ToListAsync(cancellationToken);
        }
    }
}
