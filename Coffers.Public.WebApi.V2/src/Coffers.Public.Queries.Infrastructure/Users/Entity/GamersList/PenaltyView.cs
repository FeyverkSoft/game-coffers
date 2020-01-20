using System;
using System.Collections.Generic;
using System.Text;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.GamersList
{
    class PenaltyView
    {
        public static String Sql = @"
select 
    p.Id,
    p.Description,
    p.`PenaltyStatus` as Status,
    p.Amount,
    p.UserId,
    p.CreateDate
from `Penalty` p
where 1 = 1
and p.UserId in @UserIds
and (
    (@Date >= p.CreateDate
    and p.CreateDate < ADDDATE(@Date, INTERVAL 1 MONTH))
    or p.PenaltyStatus in ('Active')
)
";

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTime CreateDate { get; }
        public PenaltyStatus Status { get; }
        public Decimal Amount { get; }
        public String Description { get; }
    }
}
