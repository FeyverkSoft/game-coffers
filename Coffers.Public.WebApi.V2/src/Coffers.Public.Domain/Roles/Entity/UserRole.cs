using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Roles
{
    public sealed class UserRole
    {
        public GamerRank UserRoleId { get; }
        public Guid GuildId { get; }
        public Guid TariffId { get; }
        public Tariff Tariff { get; }
        private UserRole() { }
        public UserRole(GamerRank userRoleId, Tariff tariff)
            => (UserRoleId, Tariff)
            = (userRoleId, tariff);
    }
}
