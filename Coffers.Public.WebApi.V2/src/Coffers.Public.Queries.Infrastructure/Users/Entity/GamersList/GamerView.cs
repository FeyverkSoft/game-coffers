using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Users.Entity.GamersList
{
    internal sealed class GamerView
    {
        public static readonly String Sql = @"
SELECT 
    u.Id,
    u.Rank,
    u.Status,
    u.DateOfBirth,
    u.Name,
    COALESCE(SUM(uo.Amount),0) AS Balance
FROM `User` u
LEFT JOIN `Operation` uo ON uo.UserId = u.Id 
                         AND uo.Type IN ('Emission', 'Other') 
                         AND uo.CreateDate < ADDDATE(@DeleteDate, INTERVAL 1 MONTH)
WHERE 1 = 1
    AND u.GuildId = @GuildId
    AND (u.DeletedDate IS NULL OR u.DeletedDate >= @DeleteDate)
    AND u.CreateDate < ADDDATE(@DeleteDate, INTERVAL 1 MONTH)
    AND u.Status IN @Statuses
GROUP BY u.Id
ORDER BY u.Rank, u.Status, u.Name
";

        public Guid Id { get; }
        public GamerStatus Status { get; }
        public GamerRank Rank { get; }
        public String Name { get; }
        public Decimal Balance { get; }
        public DateTime DateOfBirth { get; }
    }
}
