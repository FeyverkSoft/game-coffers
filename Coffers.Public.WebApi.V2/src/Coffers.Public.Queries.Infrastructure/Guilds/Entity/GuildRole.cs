using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Infrastructure.Guilds.Entity
{
    internal sealed class GuildRole
    {
        public static String Sql = @"SELECT
    ur.Id AS UserRoleId,
    ut.LoanTax AS LoanTax,
    ut.ExpiredLoanTax AS ExpiredLoanTax,
    ut.Tax AS Tax
FROM `UserRole` ur
LEFT JOIN Tariff ut ON ut.`Id` = `ur`.`TariffId`
WHERE 1 = 1
AND ur.GuildId = @GuildId
";
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
        public String Tax { get; }
    }
}
