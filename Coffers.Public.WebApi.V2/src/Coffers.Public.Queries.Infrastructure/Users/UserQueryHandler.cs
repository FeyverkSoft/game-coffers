using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.Queries.Infrastructure.Guilds.Entity;
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
            var result = await _db.QueryAsync<GamerView>(GamerView.Sql, new
            {
                GuildId = query.GuildId,
                DeleteDate = query.DateMonth ?? DateTime.UtcNow.Trunc(DateTruncType.Month),
                Statuses = query.GamerStatuses.Any() ? query.GamerStatuses.Select(_=>_.ToString()) : Enum.GetNames(typeof(GamerStatus))
            });
            return null;
        }
    }
}
