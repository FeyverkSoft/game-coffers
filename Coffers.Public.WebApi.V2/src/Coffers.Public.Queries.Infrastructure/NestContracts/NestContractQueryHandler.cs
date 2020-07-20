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
        IQueryHandler<NestContractsQuery, IEnumerable<NestContractView>>
    {
        private readonly IDbConnection _db;

        public NestContractQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<NestContractView> IQueryHandler<NestContractQuery, NestContractView>.Handle(NestContractQuery query, CancellationToken cancellationToken)
        {
            var nestContract = await _db.QuerySingleOrDefaultAsync<Entity.NestContract>(Entity.NestContract.Sql, new
            {
                NestContractId = query.NestContractId,
                GuildId = query.GuildId
            });
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
            var nest = await _db.QueryAsync<Entity.Nest>(Entity.Nest.Sql, new
            {
                GuildId = query.GuildId
            });
            return nest.Select(_ => new NestView(
                id: _.Id,
                name: _.Name,
                guildId: query.GuildId));
        }

        async Task<IEnumerable<NestContractView>> IQueryHandler<NestContractsQuery, IEnumerable<NestContractView>>.Handle(NestContractsQuery query,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}