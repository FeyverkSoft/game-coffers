using System;

namespace Coffers.Public.Queries.Infrastructure.User.Entity
{
    internal sealed class GamerView
    {
        public static String Sql = @"SELECT 
  u.Id,
  u.Rank,
  u.Status,
  u.DateOfBirth,
  u.Name,
  SUM(uo.Amount) AS Balance
FROM `User` u
LEFT JOIN `Operation` uo ON uo.UserId = u.Id AND uo.Type IN ('Emission', 'Other')
WHERE 1 = 1
  AND u.GuildId = @GuildId
  AND (u.DeleteDate IS NULL OR u.DeleteDate < @DeleteDate)
  AND o.CreateDate < ADDDATE(@DeleteDate, MONTH 1)";
    }
}
