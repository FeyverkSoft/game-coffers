using System;
namespace Coffers.Public.Queries.Infrastructure.Users.Entity.GamersList
{
    internal sealed class CharacterView
    {
        internal static readonly String Sql = @"
select 
    c.Id,
    c.Name, 
    c.ClassName,
    c.IsMain,
    c.UserId
from `Character`c
where 1 = 1
and c.UserId in @UserIds
and c.Status in @Statuses
";
        public Guid Id { get; }
        public Guid UserId { get; }
        public String Name { get; }
        public String ClassName { get; }
        public Boolean IsMain { get; }
    }
}
