using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Guilds
{
    public sealed class GuildRoleView
    {
        public Guid GuildId { get; }
        /// <summary>
        /// Роль
        /// </summary>
        public GamerRank UserRoleId { get; }
        /// <summary>
        /// Стоимость займа в процентах за день
        /// </summary>
        public Decimal LoanTax { get; }
        /// <summary>
        /// Сумма просрочки займа в процентах в день
        /// </summary>
        public Decimal ExpiredLoanTax { get; }
        /// <summary>
        /// Налог за чаров
        /// </summary>
        public ICollection<Decimal> Tax { get; }

        public GuildRoleView(Guid guildId, GamerRank userRoleId, Decimal loanTax, Decimal expiredLoanTax, ICollection<Decimal> tax)
            => (GuildId, UserRoleId, LoanTax, ExpiredLoanTax, Tax)
            =  (guildId, userRoleId, loanTax, expiredLoanTax, tax);
    }
}
