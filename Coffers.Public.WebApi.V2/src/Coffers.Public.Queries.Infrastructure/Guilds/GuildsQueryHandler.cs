using System;
using System.Collections.Generic;
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
        IQueryHandler<GuildBalanceQuery, GuildBalanceView>,
        IQueryHandler<GuildRoleListQuery, ICollection<GuildRoleView>>
    {
        private readonly IDbConnection _db;

        public GuildsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            var result = await _db.QuerySingleAsync<Guild>(Guild.Sql, new { GuildId = query.GuildId });
            return new GuildView(result.Id, result.Name, result.RecruitmentStatus, result.GamersCount, result.CharactersCount);
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

        public async Task<ICollection<GuildRoleView>> Handle(GuildRoleListQuery query, CancellationToken cancellationToken)
        {
            var result = await _db.QueryAsync<GuildRole>(GuildRole.Sql, new { GuildId = query.GuildId });
            return result.Select(_ => new GuildRoleView(_.UserRoleId, _.LoanTax, _.ExpiredLoanTax, _.Tax?.TryParseJson<Decimal[]>())).ToList();
        }
    }
}
