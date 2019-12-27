using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.Queries.Infrastructure.Guilds.Entity;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public class GuildsQueryHandler :
        IQueryHandler<GuildQuery, GuildView>,
        IQueryHandler<GuildBalanceQuery, GuildBalanceView>
    {
        private readonly IDbConnection _db;

        public GuildsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            var sql = @"
SELECT
    g.`Id`, 
    g.`Name`, 
    g.`RecruitmentStatus`, 
    count(u.`id`)  AS GamersCount, 
    count(ch.`id`) AS CharactersCount 
FROM `Guild` g
LEFT JOIN `User` u ON u.`GuildId` = g.`id` AND u.`Status` NOT IN ( 'Banned', 'Left' ) 
LEFT JOIN `Character` ch ON u.`Id` = ch.`UserId` AND ch.`Status` = 'Active' 
WHERE g.`id` = @GuildId
";
            return await _db.QuerySingleAsync<GuildView>(sql, new { GuildId = query.GuildId });
        }

        public async Task<GuildBalanceView> Handle(GuildBalanceQuery query, CancellationToken cancellationToken)
        {
            var chList = await _db.QueryAsync<UserTaxList>(UserTaxList.Sql, new { GuildId = query.GuildId });
            var guildBalance = await _db.QuerySingleAsync<GuildBalance>(GuildBalance.Sql, new { GuildId = query.GuildId, Data = DateTime.UtcNow.Trunc(DateTruncType.Month) });
            var expectedTaxAmount = (
                    from userTaxList in chList
                    let tax = userTaxList.Tax?.ParseJson<Decimal[]>() ?? new Decimal[] { }
                    let fee = tax.Length < userTaxList.CharCount ? tax.LastOrDefault<Decimal>() : tax[userTaxList.CharCount]
                    select userTaxList.CharCount * fee
                 ).Sum();
            return new GuildBalanceView(
                query.GuildId,
                guildBalance.Balance,
                expectedTaxAmount,
                guildBalance.TaxAmount,
                guildBalance.ActiveLoansAmount,
                guildBalance.RepaymentLoansAmount,
                guildBalance.GamersBalance);
        }
    }
}
