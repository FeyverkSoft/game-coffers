using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Queries.NestContract;

using Query.Core;

using Dapper;

namespace Coffers.Public.Queries.Infrastructure.NestContracts
{
    public sealed class NestContractQueryHandler :
        IQueryHandler<NestContractQuery, NestContractView>,
        IQueryHandler<NestsQuery, IEnumerable<NestView>>,
        IQueryHandler<NestContractsQuery, IEnumerable<NestContractView>>,
        IQueryHandler<GuildNestContractsQuery, IDictionary<String, IEnumerable<GuildNestContractView>>>
    {
        private readonly IDbConnection _db;

        public NestContractQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<NestContractView> IQueryHandler<NestContractQuery, NestContractView>.Handle(NestContractQuery query, CancellationToken cancellationToken)
        {
            var nestContract = await _db.QuerySingleOrDefaultAsync<Entity.NestContract>(new CommandDefinition(
                commandText: Entity.NestContract.Sql,
                parameters: new
                {
                    NestContractId = query.NestContractId,
                    GuildId = query.GuildId
                },
                commandType: CommandType.Text,
                cancellationToken: cancellationToken
            ));
            return new NestContractView(
                id: nestContract.Id,
                userId: nestContract.UserId,
                reward: nestContract.Reward,
                characterName: nestContract.CharacterName,
                nestName: nestContract.NestName
            );
        }

        async Task<IEnumerable<NestView>> IQueryHandler<NestsQuery, IEnumerable<NestView>>.Handle(NestsQuery query, CancellationToken cancellationToken)
        {
            var nest = await _db.QueryAsync<Entity.Nest>(
                new CommandDefinition(
                    commandText: Entity.Nest.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return nest.Select(_ => new NestView(
                id: _.Id,
                name: _.Name,
                guildId: query.GuildId));
        }

        async Task<IEnumerable<NestContractView>> IQueryHandler<NestContractsQuery, IEnumerable<NestContractView>>.Handle(NestContractsQuery query,
            CancellationToken cancellationToken)
        {
            var nestContracts = await _db.QueryAsync<Entity.UserNestContracts>(
                new CommandDefinition(
                    commandText: Entity.UserNestContracts.Sql,
                    parameters: new
                    {
                        UserId = query.UserId,
                        GuildId = query.GuildId
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            return nestContracts.Select(nestContract => new NestContractView(
                id: nestContract.Id,
                userId: nestContract.UserId,
                reward: nestContract.Reward,
                characterName: nestContract.CharacterName,
                nestName: nestContract.NestName
            ));
        }

        async Task<IDictionary<String, IEnumerable<GuildNestContractView>>>
            IQueryHandler<GuildNestContractsQuery, IDictionary<String, IEnumerable<GuildNestContractView>>>.Handle(
                GuildNestContractsQuery query,
                CancellationToken cancellationToken)
        {
            var nestContracts = await _db.QueryAsync<Entity.GuildNestContracts>(
                new CommandDefinition(
                    commandText: Entity.GuildNestContracts.Sql,
                    parameters: new
                    {
                        GuildId = query.GuildId
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));

            return nestContracts.GroupBy(_ => _.NestName, nestContract => new GuildNestContractView(
                id: nestContract.Id,
                reward: nestContract.Reward,
                characterName: nestContract.CharacterName
            )).ToDictionary(_ => _.Key, _ => (IEnumerable<GuildNestContractView>) _.ToList());
        }
    }
}