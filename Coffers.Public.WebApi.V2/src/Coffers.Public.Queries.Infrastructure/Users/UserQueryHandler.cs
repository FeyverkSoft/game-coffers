using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Infrastructure.Users.Entity;
using Coffers.Public.Queries.Users;
using Coffers.Types.Gamer;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Users
{
    public class UserQueryHandler :
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>
    {
        private readonly IDbConnection _db;

        public UserQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<ICollection<GamersListView>> Handle(GetGamersQuery query, CancellationToken cancellationToken)
        {
            var users = await _db.QueryAsync<Entity.GamersList.GamerView>(Entity.GamersList.GamerView.Sql, new
            {
                GuildId = query.GuildId,
                DeleteDate = query.DateMonth ?? DateTime.UtcNow.Trunc(DateTruncType.Month),
                Statuses = query.GamerStatuses.Any() ? query.GamerStatuses.Select(_ => _.ToString()) : Enum.GetNames(typeof(GamerStatus))
            });

            var userIds = users.Select(_ => _.Name);
            var characters = await _db.QueryAsync<Entity.GamersList.CharacterView>(Entity.GamersList.CharacterView.Sql, new
            {
                UserIds = userIds,
                Statuses = new[] { CharStatus.Active }
            });
            var loans = await _db.QueryAsync<Entity.GamersList.LoanView>(Entity.GamersList.LoanView.Sql, new
            {
                UserIds = userIds,
                Date = query.DateMonth
            });
            var penalties = await _db.QueryAsync<Entity.GamersList.PenaltyView>(Entity.GamersList.PenaltyView.Sql, new
            {
                UserIds = userIds,
                Date = query.DateMonth
            });
            return null;
        }
    }
}
