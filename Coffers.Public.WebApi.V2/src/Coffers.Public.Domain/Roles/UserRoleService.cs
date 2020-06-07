using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Helpers;
using Coffers.Public.Domain.Roles.Entity;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Roles
{
    public static class UserRoleService
    {
        public static void AddOrUpdateRole(this Guild guild, GamerRank rank, Decimal loanTax, Decimal expiredLoanTax, ICollection<Decimal> tax)
        {
            if (guild.Roles.Any(_ => _.UserRoleId == rank))
                guild.DeleteRole(rank);
            var roles = new UserRole(rank, new Tariff(loanTax, expiredLoanTax, tax.ToJson()));
            guild.AddNewRole(roles);
        }
    }
}
