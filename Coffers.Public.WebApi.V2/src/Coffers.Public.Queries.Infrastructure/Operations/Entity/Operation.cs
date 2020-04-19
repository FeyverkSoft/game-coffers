using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Operations.Entity
{
    internal sealed class OperationListItem
    {
        internal static readonly String Sql = @"
select 
    op.`Id`,
    op.`Amount`,
    op.`CreateDate`,
    op.`Description`,
    op.`Type`,
    op.`DocumentId`,
    u.`Name` as UserName,
    u.`Id` as UserId,
    coalesce(l.`Description`, p.`Description`) as DocumentDescription,
    coalesce(l.`Amount`, p.`Amount`, t.`Amount`) as DocumentAmount    
from operation op
join user u on op.`UserId` = u.`Id`
left join loan l on l.`Id` = op.`DocumentId`
left join tax t on t.`Id` = op.`DocumentId`
left join penalty p on p.`Id` = op.`DocumentId`
where 1 = 1
    AND u.GuildId = @GuildId
    AND op.CreateDate >= @DateMonth
    AND op.CreateDate < ADDDATE(@DateMonth, INTERVAL 1 MONTH)
order by op.`CreateDate`
";

        public Guid Id { get; }
        public Decimal Amount { get; }
        public DateTime CreateDate { get; }
        public String Description { get; }
        public OperationType Type { get; }
        public Guid? DocumentId { get; }
        public Decimal? DocumentAmount { get; }
        public String DocumentDescription { get; }
        public Guid UserId { get; }
        public String UserName { get; }
    }
}
