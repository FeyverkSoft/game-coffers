using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Infrastructure.Operations;
using Coffers.Public.Queries.Gamers;
using Coffers.Types.Account;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Gamers
{
    public sealed class GamerQueryHandler : IQueryHandler<GetBaseGamerInfoQuery, BaseGamerInfoView>,
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>,
        IQueryHandler<GetGamerInfoQuery, GamerInfoView>

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
                .Include(_ => _.Penalties)
                .Include(_ => _.Loans)
                .Include(_ => _.Characters)
                .Include(_ => _.DefaultAccount)
                .ThenInclude(_ => _.FromOperations)
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
                    RepaymentTaxAmount = g.DefaultAccount.FromOperations
                        .Where(_ => _.Type == OperationType.Tax
                                  && _.CreateDate >= DateTime.UtcNow.Trunc(DateTruncType.Month))
                        .Sum(_ => _.Amount)
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

#warning прячим служебного временного пользователя
            q = q.Where(g => g.Name!= "user");

            if (query.DateTo != null)
                q = q.Where(g => g.CreateDate <= query.DateTo.Value.Trunc(DateTruncType.Month));

            q = q.Include(g => g.DefaultAccount)
             .Include(g => g.Characters)
             .Include(g => g.Loans)
             .Include(g => g.Penalties);
            return await q
            .OrderBy(_ => _.Rank).ThenBy(_ => _.Status).ThenBy(_ => _.CreateDate)
            .Select(g => new GamersListView
            {
                Id = g.Id,
                Name = g.Name,
                Balance = g.DefaultAccount.Balance,
                Characters = g.Characters.Where(c => c.Status == CharStatus.Active).Select(x => x.Name).ToList(),
                Rank = g.Rank,
                Status = g.Status,
                DateOfBirth = g.DateOfBirth.Trunc(DateTruncType.Day),
                Penalties = g.Penalties.Where(p => p.CreateDate >= dateFrom || p.PenaltyStatus == PenaltyStatus.Active)
                    .Select(p => new PenaltyView
                    {
                        Id = p.Id,
                        Amount = p.Amount,
                        Date = p.CreateDate,
                        Description = p.Description,
                        PenaltyStatus = p.PenaltyStatus
                    }).OrderBy(_ => _.Date).ToList(),
                Loans = g.Loans.Where(l => l.CreateDate >= dateFrom || l.LoanStatus == LoanStatus.Active || l.LoanStatus == LoanStatus.Expired)
                    .Select(l => new LoanView
                    {
                        Amount = l.Amount,
                        Date = l.CreateDate,
                        Description = l.Description,
                        LoanStatus = l.LoanStatus,
                        ExpiredDate = l.ExpiredDate,
                        Id = l.Id
                    }).OrderBy(_ => _.Date).ToList(),
            })
                .ToListAsync(cancellationToken);
        }

        public async Task<GamerInfoView> Handle(GetGamerInfoQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Gamers
                .AsNoTracking()
                .Include(_ => _.DefaultAccount)
                .Where(g => g.Id == query.UserId);

            return await q
                .Select(_ => new GamerInfoView
                {
                    UserId = _.Id,
                    AccountId = _.DefaultAccount.Id,
                    GuildId = _.GuildId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
