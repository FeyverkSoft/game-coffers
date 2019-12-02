using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Profiles;
using Coffers.Types.Account;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Queries.Infrastructure.Profiles
{
    public sealed class ProfilesQueryHandler : IQueryHandler<GetBaseGamerInfoQuery, BaseGamerInfoView>

    {
        private readonly ProfilesQueryDbContext _context;
        public ProfilesQueryHandler(ProfilesQueryDbContext context)
        {
            _context = context;
        }

        public async Task<BaseGamerInfoView> Handle(GetBaseGamerInfoQuery query, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow.Trunc(DateTruncType.Month);
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
                        .Sum(l => l.Amount),
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
                                  && _.CreateDate >= now)
                        .Sum(_ => _.Amount)
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
