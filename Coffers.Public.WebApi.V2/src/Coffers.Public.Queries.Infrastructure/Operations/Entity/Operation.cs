using System;

namespace Coffers.Public.Queries.Infrastructure.Operations.Entity
{
    internal sealed class Operation
    {
        internal static String Sql = @"
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
    AND (@Id IS NULL OR op.`Id` = @Id)
    AND (u.DeletedDate IS NULL OR u.DeletedDate < @DeleteDate)
    AND u.CreateDate < ADDDATE(@DeleteDate, INTERVAL 1 MONTH)
order by op.`CreateDate`
";

    }
}
