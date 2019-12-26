using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Guilds;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Guilds
{
    public class GuildsQueryHandler :
        IQueryHandler<GuildQuery, GuildView>,
        IQueryHandler<GuildBalanceQuery, GuildBalanceView>
    {
        private readonly IDbConnection _db;

        public GuildsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<GuildView> Handle(GuildQuery query, CancellationToken cancellationToken)
        {
            var sql = @"
SELECT
    g.`Id`, 
    g.`Name`, 
    g.`RecruitmentStatus`, 
    count(u.`id`)  AS GamersCount, 
    count(ch.`id`) AS CharactersCount 
FROM `guild` g
LEFT JOIN `user` u ON u.`GuildId` = g.`id` AND u.`status` NOT IN ( 'Banned', 'Left' ) 
LEFT JOIN `character` ch ON u.`id` = ch.`userid` AND ch.`status` = 'Active' 
WHERE g.`id` = '@GuildId'
) gi
";
            return await _db.QuerySingleAsync<GuildView>(sql, new { GuildId = query.GuildId });
        }

        public async Task<GuildBalanceView> Handle(GuildBalanceQuery query, CancellationToken cancellationToken)
        {
            var sql = @"
SELECT
    g.`Id`, 
    g.`Name`, 
    g.`RecruitmentStatus`, 
    count(u.`id`)  AS GamersCount, 
    count(ch.`id`) AS CharactersCount 
FROM `guild` g
LEFT JOIN `user` u ON u.`GuildId` = g.`id` AND u.`status` NOT IN ( 'Banned', 'Left' ) 
LEFT JOIN `character` ch ON u.`id` = ch.`userid` AND ch.`status` = 'Active' 
WHERE g.`id` = '@GuildId'
) gi
";
            return await _db.QuerySingleAsync<GuildBalanceView>(sql,
                new
                {
                    GuildId = query.GuildId
                });
        }
    }
}
