using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Queries.Penalties;

using Query.Core;

using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Penalties
{
    public sealed class PenaltyQueryHandler : IQueryHandler<PenaltyViewQuery, PenaltyView>
    {
        private readonly IDbConnection _db;

        public PenaltyQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<PenaltyView> IQueryHandler<PenaltyViewQuery, PenaltyView>.Handle(PenaltyViewQuery query, CancellationToken cancellationToken)
        {
            var penalty = await _db.QueryFirstOrDefaultAsync<Entity.PenaltyView>(
                new CommandDefinition(
                    commandText: Entity.PenaltyView.Sql,
                    parameters: new
                    {
                        PenaltyId = query.PenaltyId,
                    },
                    commandType: CommandType.Text,
                    cancellationToken: cancellationToken
                ));
            return new PenaltyView(penalty.Id, penalty.Amount, penalty.CreateDate, penalty.Description, penalty.Status);
        }
    }
}