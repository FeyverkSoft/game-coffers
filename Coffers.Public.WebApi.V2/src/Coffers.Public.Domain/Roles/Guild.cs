using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Roles
{
    public sealed class Guild
    {
        /// <summary>
        /// Id гильдии
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Список ролей игроков в гильдии
        /// </summary>
        public ICollection<UserRole> Roles { get; } = new List<UserRole>();
        public Guid ConcurrencyTokens { get; private set; }

        internal void AddNewRole(UserRole role)
        {
            if (Roles.Any(_ => _.UserRoleId == role.UserRoleId))
                throw new InvalidOperationException("Role already exists");

            Roles.Add(role);
            ConcurrencyTokens = Guid.NewGuid();
        }

        internal void DeleteRole(GamerRank rank)
        {
            if (Roles.All(_ => _.UserRoleId != rank))
                throw new InvalidOperationException("Role not found");

            Roles.Remove(Roles.Single(_ => _.UserRoleId == rank));
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
