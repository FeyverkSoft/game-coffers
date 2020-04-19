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
from `user` u
left join `userrole` ur on  ur.`Id` = u.`Rank` 
                   and ur.`GuildId` = u.`GuildId` 
left join `character` uch on  uch.`UserId` = u.`Id` 
                          and uch.`Status` = 'Active'
left join `tariff` t on t.Id = ur.`TariffId`
where 1 = 1
    and u.`Id` = @UserId
    and u.`GuildId` = @GuildId
";
        public Guid UserId { get; }
        public Int32 CharCount { get; }
        public String TaxTariff { get; }
    }
}
