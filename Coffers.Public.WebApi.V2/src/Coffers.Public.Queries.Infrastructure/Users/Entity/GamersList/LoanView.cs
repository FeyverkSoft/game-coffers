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
    l.UserId,
    l.ExpiredDate
from `Loan`l
where 1 = 1
and l.UserId in @UserIds
and (
    (@Date >= l.CreateDate
    and l.CreateDate < ADDDATE(@Date, INTERVAL 1 MONTH))
    or l.LoanStatus in ('Active', 'Expired')
)
";

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTime ExpiredDate { get; }
        public LoanStatus Status { get; }
        public Decimal Amount { get; }
        public String Description { get; }
    }
}
