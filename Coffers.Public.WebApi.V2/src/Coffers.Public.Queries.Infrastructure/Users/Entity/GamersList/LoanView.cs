using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.GamersList
{
    internal sealed class LoanView
    {
        public static String Sql = @"
select 
    l.Id,
    l.Description,
    l.LoanStatus as Status,
    l.Amount,
    l.UserId
from `Loan`l
where 1 = 1
and l.UserId in @Ids
and (
    (@Date >= uo.CreateDate
    and uo.CreateDate < ADDDATE(@Date, INTERVAL 1 MONTH))
    or l.LoanStatus in ('Active', 'Expired')
)
";

        public Guid Id { get; }
        public Guid UserId { get; }
        public LoanStatus Status { get; }
        public Decimal Amount { get; }
        public String Description { get; }
    }
}
