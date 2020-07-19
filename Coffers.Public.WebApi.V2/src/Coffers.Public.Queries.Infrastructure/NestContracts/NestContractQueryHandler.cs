using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Loans;
using Coffers.Public.Queries.NestContract;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.NestContracts
{
    public sealed class NestContractQueryHandler : IQueryHandler<NestContractQuery, NestContractView>
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
            return new NestContractView();
        }
    }
}