using System;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class UserRole
    {
        public GamerRank UserRoleId { get; }
        public Guid GuildId { get; }
        public Guid TariffId { get; }
    }
}
