﻿using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Operations.Entity
{
    internal sealed class OperationItem
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
    coalesce(l.`Amount`, p.`Amount`, t.`Amount`) as DocumentAmount    
from Operation op
join User u on op.`UserId` = u.`Id`
left join Loan l on l.`Id` = op.`DocumentId`
left join Tax t on t.`Id` = op.`DocumentId`
left join Penalty p on p.`Id` = op.`DocumentId`
where 1 = 1
    AND u.GuildId = @GuildId
    AND op.Id = @OperationId
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
