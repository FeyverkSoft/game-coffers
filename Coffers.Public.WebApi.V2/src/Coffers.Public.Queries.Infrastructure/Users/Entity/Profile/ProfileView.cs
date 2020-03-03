using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.Profile
{
   internal sealed class ProfileView
    {
        public static String Sql = @"
select
    u.`Id` as UserId,
    u.`Name` as Name,
    u.`Rank` as Rank,
    count(uch.`Id`) as CharCount,
    u.`DateOfBirth` as DateOfBirth,
    coalesce(sum(uo.`Amount` ), 0.0) as Balance,
    count(ul.`Id`) as ActiveLoanAmount,
    coalesce(sum(up.`Amount`), 0.0) as ActivePenaltyAmount,
    coalesce(sum(ul.`TaxAmount`), 0.0) as ActiveLoanTaxAmount,
    uc.`Name` as CharacterName
from `user` u
left join `operation` uo  on  uo.`UserId` = u.`Id`
                          and uo.Type in ('Emission', 'Other')
left join `character` uch on  uch.`UserId` = u.`Id` 
                          and uch.`Status` = 'Active'
left join `loan` ul on ul.`LoanStatus` in ('Active', 'Expired') 
left join `penalty` up on  up.`UserId` = u.`Id` 
                       and up.`PenaltyStatus` = 'Active'
left join ( select 
                c.`Name`,
                c.`UserId`
            from   `character` c 
            where 1 = 1
                and c.`IsMain` = 1
                and c.`Status` = 'Active'
            limit 1) as uc on uc.`UserId` = u.`Id`
where 1 = 1
    and u.`Id` = @UserId
    and u.`GuildId` = @GuildId
";
        public Guid UserId { get; }
        public String Name { get; }
        public GamerRank Rank { get; }
        public Int32 CharCount { get; }
        public Decimal Balance { get; }
        public Decimal ActiveLoanAmount { get; }
        public Decimal ActivePenaltyAmount { get; }
        public Decimal ActiveLoanTaxAmount { get; }

        /// <summary>
        /// Имя основного персонажа
        /// </summary>
        public String CharacterName { get; }
        public DateTime DateOfBirth { get; }
    }
}
