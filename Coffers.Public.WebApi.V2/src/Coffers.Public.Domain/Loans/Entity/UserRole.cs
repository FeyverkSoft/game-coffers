using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Loans.Entity
{
    public sealed class UserRole
    {
        public GamerRank UserRoleId { get; }
        public Guid GuildId { get; }
        public Guid TariffId { get; }
        public Tariff Tariff { get; }
    }
}
