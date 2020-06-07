using System;

namespace Coffers.Public.Queries.Infrastructure.Guilds.Entity.GuildBalance
{
    internal sealed class UserTaxList
    {
        public static readonly String Sql = @"
SELECT 
    u.`Id` AS UserId, 
    ut.`Tax` AS Tax,
    COUNT(ch.`Id`) AS CharCount 
FROM `User` u
LEFT JOIN `Character` ch ON ch.`UserId` = u.`Id`
LEFT JOIN `UserRole` ur ON ur.`Id` = u.`Rank` AND  ur.`GuildId` = u.`GuildId`
LEFT JOIN `Tariff` ut ON ut.`Id` = `ur`.`TariffId`
WHERE 1 = 1
AND u.`Status` NOT IN ('Left', 'Banned', 'Spirit')
AND ch.`Status` = 'Active'
AND u.`GuildId` = @GuildId
GROUP BY u.`Id`
";
        public Guid UserId { get; }
        public String Tax { get; }
        public Int32 CharCount { get; }
        protected UserTaxList() { }
    }
}
