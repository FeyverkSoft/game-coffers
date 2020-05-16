using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Operations.Entity
{
    internal sealed class OperationListItem
    {
        internal static readonly String Sql = @"
select 
    op.`Id` as Id,
    op.`Amount` as Amount,
    op.`CreateDate` as CreateDate,
    op.`Description` as Description,
    op.`Type` as Type,
    op.`DocumentId` as DocumentId,
    u.`Name` as UserName,
    u.`Id` as UserId,
    coalesce(l.`Description`, p.`Description`) as DocumentDescription,
    coalesce(l.`Amount`, p.`Amount`, t.`Amount`) as DocumentAmount,
    op.`ParentOperationId` as ParentOperationId,
    pop.`Description` as ParentOperationDescription
from Operation op
join User u on op.`UserId` = u.`Id`
left join Operation pop on pop.`Id` = op.`ParentOperationId`
left join Loan l on l.`Id` = op.`DocumentId`
left join Tax t on t.`Id` = op.`DocumentId`
left join Penalty p on p.`Id` = op.`DocumentId`
where 1 = 1
    AND u.GuildId = @GuildId
    AND (op.CreateDate >= @DateMonth
    AND op.CreateDate < ADDDATE(@DateMonth, INTERVAL 1 MONTH))
order by op.`CreateDate` desc 
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
        public Guid? ParentOperationId { get; }
        public String ParentOperationDescription { get; }
    }
}