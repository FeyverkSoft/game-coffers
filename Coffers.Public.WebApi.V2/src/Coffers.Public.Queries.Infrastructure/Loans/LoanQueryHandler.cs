using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Loans;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Loans
{
    public sealed class LoanQueryHandler : IQueryHandler<LoanViewQuery, LoanView>
    {
        private readonly IDbConnection _db;

        public LoanQueryHandler(IDbConnection db)
        {
            _db = db;
        }
        async Task<LoanView> IQueryHandler<LoanViewQuery, LoanView>.Handle(LoanViewQuery query, CancellationToken cancellationToken)
        {
            var loan = await _db.QueryFirstOrDefaultAsync<Entity.LoanView>(Entity.LoanView.Sql, new
            {
                LoanId = query.LoanId,
            });
            return new LoanView(loan.Id, loan.Amount, loan.Balance, loan.Description, loan.Status, loan.CreateDate, loan.ExpiredDate);
        }
    }
}