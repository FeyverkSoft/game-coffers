using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Gamers;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Gamers
{
    public sealed class GamerQueryHandler : IQueryHandler<GetBaseGamerInfoQuery, BaseGamerInfoView>
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
                        .Sum(l => l.Amount - l.RedemptionAmount),
                    ActivePenaltyAmount = g.Penalties.Where(p => p.PenaltyStatus == PenaltyStatus.Active)
                        .Sum(p => p.Amount - p.RedemptionAmount)
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
