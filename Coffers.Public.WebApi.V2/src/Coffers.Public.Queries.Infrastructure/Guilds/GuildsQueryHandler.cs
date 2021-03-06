﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Helpers;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.Queries.Infrastructure.Guilds.Entity.Guild;
using Coffers.Public.Queries.Infrastructure.Guilds.Entity.GuildBalance;

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
            var result = await _db.QuerySingleAsync<Guild>(Guild.Sql, new {GuildId = query.GuildId});
            return new GuildView(result.Id, result.Name, result.RecruitmentStatus, result.GamersCount, result.CharactersCount);
        }

        public async Task<GuildBalanceView> Handle(GuildBalanceQuery query, CancellationToken cancellationToken)
        {
            var chList = await _db.QueryAsync<UserTaxList>(new CommandDefinition(
                commandText: UserTaxList.Sql,
                parameters: new {GuildId = query.GuildId},
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
            var guildBalance =
                await _db.QuerySingleAsync<GuildBalance>(GuildBalance.Sql, new
                {
                    GuildId = query.GuildId,
                    Data = DateTime.UtcNow.Trunc(DateTruncType.Month)
                });
            var expectedTaxAmount = (
                from userTaxList in chList
                let tax = userTaxList.Tax?.ParseJson<Decimal[]>() ?? new Decimal[] { }
                let fee = tax.Length < userTaxList.CharCount ? tax.LastOrDefault<Decimal>() : tax[userTaxList.CharCount - 1]
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
            var result = await _db.QueryAsync<GuildRole>(new CommandDefinition(
                commandText: GuildRole.Sql,
                parameters: new
                {
                    GuildId = query.GuildId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
            return result.Select(_ => new GuildRoleView(query.GuildId, _.UserRoleId, _.LoanTax, _.ExpiredLoanTax, _.Tax?.TryParseJson<Decimal[]>())).ToList();
        }
    }
}