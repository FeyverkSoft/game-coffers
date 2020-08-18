using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Helpers;
using Coffers.Public.Queries.Users;
using Coffers.Types.Gamer;

using Query.Core;

using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Users
{
    public sealed class UserQueryHandler :
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>,
        IQueryHandler<ProfileViewQuery, ProfileView>,
        IQueryHandler<CharacterViewQuery, IEnumerable<CharacterView>>,
        IQueryHandler<UserTaxViewQuery, UserTaxView>

    {
        private readonly IDbConnection _db;

        public UserQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<ICollection<GamersListView>> IQueryHandler<GetGamersQuery, ICollection<GamersListView>>.Handle(GetGamersQuery query,
            CancellationToken cancellationToken)
        {
            var dateMonth = (query.DateMonth ?? DateTime.UtcNow).Trunc(DateTruncType.Month);

            var users = await _db.QueryAsync<Entity.GamersList.GamerView>(
                new CommandDefinition(
                    commandText: Entity.GamersList.GamerView.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId,
                        DeleteDate = dateMonth,
                        Statuses = query.GamerStatuses.Any() ? query.GamerStatuses.Select(_ => _.ToString()) : Enum.GetNames(typeof(GamerStatus))
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            var userIds = users.Select(_ => _.Id);
            var characters = await _db.QueryAsync<Entity.GamersList.CharacterView>(
                new CommandDefinition(
                    commandText: Entity.GamersList.CharacterView.Sql,
                    parameters: new
                    {
                        UserIds = userIds,
                        Statuses = new[] {CharStatus.Active.ToString()}
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            var loans = await _db.QueryAsync<Entity.GamersList.LoanView>(
                new CommandDefinition(
                    commandText: Entity.GamersList.LoanView.Sql,
                    parameters: new
                    {
                        UserIds = userIds,
                        Date = dateMonth
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            var penalties = await _db.QueryAsync<Entity.GamersList.PenaltyView>(
                new CommandDefinition(
                    commandText: Entity.GamersList.PenaltyView.Sql,
                    parameters: new
                    {
                        UserIds = userIds,
                        Date = dateMonth,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            return users.Select(user => new GamersListView(
                    id: user.Id,
                    name: user.Name,
                    balance: user.Balance,
                    characters: characters.Where(_ => _.UserId == user.Id)
                        .Select(_ => new CharacterView(_.Id, _.Name, _.ClassName, _.IsMain, _.UserId)),
                    rank: user.Rank,
                    status: user.Status,
                    dateOfBirth: user.DateOfBirth,
                    penalties: penalties.Where(_ => _.UserId == user.Id)
                        .Select(_ => new PenaltyView(_.Id, _.Amount, _.CreateDate, _.Description, _.Status)),
                    loans: loans.Where(_ => _.UserId == user.Id)
                        .Select(_ => new LoanView(_.Id, _.Amount, _.Balance, _.Description, _.Status, _.CreateDate, _.ExpiredDate))))
                .OrderBy(_ => _.Rank)
                .ThenBy(_ => _.Status)
                .ThenBy(_ => _.Name)
                .ToList();
        }

        async Task<ProfileView> IQueryHandler<ProfileViewQuery, ProfileView>.Handle(ProfileViewQuery query, CancellationToken cancellationToken)
        {
            var profile = await _db.QuerySingleAsync<Entity.Profile.ProfileView>(
                new CommandDefinition(
                    commandText: Entity.Profile.ProfileView.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId,
                        UserId = query.UserId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            return new ProfileView(
                profile.UserId,
                profile.Name,
                profile.Rank,
                profile.CharacterName,
                profile.CharCount,
                profile.Balance,
                profile.ActiveLoanAmount,
                profile.ActivePenaltyAmount,
                profile.ActiveLoanTaxAmount,
                profile.DateOfBirth
            );
        }

        async Task<IEnumerable<CharacterView>> IQueryHandler<CharacterViewQuery, IEnumerable<CharacterView>>.Handle(CharacterViewQuery query,
            CancellationToken cancellationToken)
        {
            var list = await _db.QueryAsync<Entity.Profile.CharacterView>(
                new CommandDefinition(
                    commandText: Entity.Profile.CharacterView.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId,
                        Statuses = new[] {CharStatus.Active.ToString()},
                        UserIds = new[] {query.UserId},
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            return list.Select(ch => new CharacterView(
                ch.Id,
                ch.Name,
                ch.ClassName,
                ch.IsMain,
                ch.UserId));
        }

        async Task<UserTaxView> IQueryHandler<UserTaxViewQuery, UserTaxView>.Handle(UserTaxViewQuery query, CancellationToken cancellationToken)
        {
            var tax = await _db.QuerySingleAsync<Entity.Profile.UserTaxView>(
                new CommandDefinition(
                    commandText: Entity.Profile.UserTaxView.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId,
                        UserId = query.UserId
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            if (String.IsNullOrEmpty(tax.TaxTariff))
                return new UserTaxView(tax.UserId, 0, new List<Decimal>());
            var taxes = tax.TaxTariff.TryParseJson<IList<Decimal>>() ?? new List<Decimal>();
            var index = tax.CharCount > taxes.Count ? taxes.Count - 1 : tax.CharCount - 1;
            var taxAmount = taxes.Count > 0 && index >= 0 ? taxes[index] * tax.CharCount : 0;
            return new UserTaxView(tax.UserId, taxAmount, taxes);
        }
    }
}