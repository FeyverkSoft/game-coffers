using System;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.Profile
{
    internal sealed class UserTaxView
    {
        internal static readonly String Sql = @"
select
    u.`Id` as UserId,
    count(uch.`Id`) as CharCount,
    t.Tax as TaxTariff
from `User` u
left join `UserRole` ur on  ur.`Id` = u.`Rank` 
                   and ur.`GuildId` = u.`GuildId` 
left join `Character` uch on  uch.`UserId` = u.`Id` 
                          and uch.`Status` = 'Active'
left join `Tariff` t on t.Id = ur.`TariffId`
where 1 = 1
    and u.`Id` = @UserId
    and u.`GuildId` = @GuildId
";
        public Guid UserId { get; }
        public Int32 CharCount { get; }
        public String TaxTariff { get; }
    }
}
