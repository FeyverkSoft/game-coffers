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
    public class UserQueryHandler :
        IQueryHandler<GetGamersQuery, ICollection<GamersListView>>,
        IQueryHandler<ProfileViewQuery, ProfileView>

    {
        private readonly IDbConnection _db;

        public UserQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        async Task<ICollection<GamersListView>> IQueryHandler<GetGamersQuery, ICollection<GamersListView>>.Handle(GetGamersQuery query, CancellationToken cancellationToken)
        {
            var dateMonth = (query.DateMonth ?? DateTime.UtcNow).Trunc(DateTruncType.Month);

            var users = await _db.QueryAsync<Entity.GamersList.GamerView>(Entity.GamersList.GamerView.Sql, new
            {
                GuildId = query.GuildId,
                DeleteDate = dateMonth,
                Statuses = query.GamerStatuses.Any() ? query.GamerStatuses.Select(_ => _.ToString()) : Enum.GetNames(typeof(GamerStatus))
            });

            var userIds = users.Select(_ => _.Id);
            var characters = await _db.QueryAsync<Entity.GamersList.CharacterView>(Entity.GamersList.CharacterView.Sql, new
            {
                UserIds = userIds,
                Statuses = new[] { CharStatus.Active.ToString() }
            });
            var loans = await _db.QueryAsync<Entity.GamersList.LoanView>(Entity.GamersList.LoanView.Sql, new
            {
                UserIds = userIds,
                Date = dateMonth
            });
            var penalties = await _db.QueryAsync<Entity.GamersList.PenaltyView>(Entity.GamersList.PenaltyView.Sql, new
            {
                UserIds = userIds,
                Date = dateMonth,
            });

            var result = new List<GamersListView>();
            foreach (var user in users)
            {
                result.Add(new GamersListView(
                    user.Id,
                    user.Name,
                    user.Balance,
                    characters.Where(_ => _.UserId == user.Id)
                        .Select(_ => new CharacterView(_.Id, _.Name, _.ClassName, _.IsMain)),
                    user.Rank,
                    user.Status,
                    user.DateOfBirth,
                    penalties.Where(_ => _.UserId == user.Id)
                        .Select(_ => new PenaltyView(_.Id, _.Amount, _.CreateDate, _.Description, _.Status)),
                    loans.Where(_ => _.UserId == user.Id)
                        .Select(_ => new LoanView(_.Id, _.Amount, _.Description, _.Status, _.ExpiredDate))
                    ));
            }
            return result;
        }

        async Task<ProfileView> IQueryHandler<ProfileViewQuery, ProfileView>.Handle(ProfileViewQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
