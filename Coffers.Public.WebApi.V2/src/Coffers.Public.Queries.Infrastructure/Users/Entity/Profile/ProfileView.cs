using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.Profile
{
    internal sealed class ProfileView
    {
        public static readonly String Sql = @"
select
    u.`Id` as UserId,
    u.`Name` as Name,
    u.`Rank` as Rank,
    u.`DateOfBirth` as DateOfBirth,
    coalesce(uch.`CharCount`, 0) as CharCount,
    coalesce(uo.`Amount`, 0.0) as Balance,
    coalesce(ul.`Amount`, 0.0) as ActiveLoanAmount,
    coalesce(up.`Amount`, 0.0) as ActivePenaltyAmount,
    coalesce(ul.`TaxAmount`, 0.0) as ActiveLoanTaxAmount,
    uc.`Name` as CharacterName
from `User` u
left join (select 
                sum(o.`Amount`) as Amount, 
                o.UserId from `Operation` o 
                where 1 = 1 
                    and o.Type in ('Emission', 'Other') 
                group by o.UserId 
            ) as uo on  uo.`UserId` = u.`Id`
left join (select 
                count(c.Id) as CharCount, 
                c.UserId  
            from `Character` c
            where 1 = 1 
                and c.`Status` = 'Active'
            group by c.UserId 
            ) as uch on  uch.`UserId` = u.`Id` 
left join (select 
                sum(l.Amount) as Amount,
                sum(l.TaxAmount) as TaxAmount,
                l.UserId 
            from `Loan` l
            where 1 = 1 
                and l.`LoanStatus` in ('Active', 'Expired')
            group by l.UserId
          ) as ul on ul.`UserId` = u.`Id`
left join (select 
                sum(p.Amount) as Amount,
                p.UserId
            from `Penalty` p
            where 1 = 1
                and p.`PenaltyStatus` = 'Active'
            group by p.UserId
            ) as up on  up.`UserId` = u.`Id`            
left join (select 
                c.`Name`,
                c.`UserId`
            from   `Character` c 
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