using System;
using Coffers.Types.Guilds;

namespace Coffers.Public.Queries.Infrastructure.Guilds.Entity
{
    internal sealed class Guild
    {
        public static readonly String Sql = @"
SELECT
    g.`Id`, 
    g.`Name`, 
    g.`RecruitmentStatus`, 
    count(u.`id`)  AS GamersCount, 
    count(ch.`id`) AS CharactersCount 
FROM `Guild` g
LEFT JOIN `User` u ON u.`GuildId` = g.`id` AND u.`Status` NOT IN ( 'Banned', 'Left' ) 
LEFT JOIN `Character` ch ON u.`Id` = ch.`UserId` AND ch.`Status` = 'Active' 
WHERE g.`id` = @GuildId
";
        public Guid Id { get; }
        public String Name { get; }
        public RecruitmentStatus RecruitmentStatus { get; }
        public Int32 GamersCount { get; }
        public Int32 CharactersCount { get; }
    }
}
