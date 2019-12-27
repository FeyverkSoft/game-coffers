using System;

namespace Coffers.Public.Queries.Infrastructure.Guilds.Entity
{
    internal sealed class GuildBalance
    {
        public static String Sql = @"
SELECT
    COALESCE(ActiveLoansAmount, 0) AS ActiveLoansAmount, /*Сумма активных займов*/
    COALESCE(RepaymentLoansAmount, 0) AS RepaymentLoansAmount, /*Сумма уже уплаченная от суммы займов*/
    COALESCE(Balance, 0) AS Balance, /*Баланс гильдии на момент запроса*/
    COALESCE(TaxAmount, 0) AS TaxAmount,  /*Уплаченная сумма налогов на текущий момент*/
    COALESCE(GamersBalance, 0) AS GamersBalance /*Балланс игроков на складе гильдии*/
FROM (
    SELECT 
        u.GuildId, 
        SUM(l.Amount) + SUM(l.PenaltyAmount) AS ActiveLoansAmount, /*Сумма активных займов*/
        SUM(case when o.Amount > 0 then  o.Amount else 0 END) AS RepaymentLoansAmount /*Сумма уже уплаченная от суммы займов*/
    FROM `User` u
    LEFT JOIN `Loan` l ON u.Id = l.UserId AND l.`LoanStatus` IN ('Active', 'Expired')
    LEFT JOIN `Operation` o ON o.`DocumentId` = l.Id 
    WHERE  1 = 1
    AND u.`GuildId` = @GuildId
) ls
LEFT JOIN (
    SELECT 
        o.`GuildId`, 
        SUM(o.Amount) AS Balance /*Баланс гильдии на момент запроса*/
    FROM   `Operation` o 
    WHERE  o.`GuildId` = @GuildId
) gs ON ls.`GuildId` = gs.`GuildId`
LEFT JOIN (
    SELECT 
        o.GuildId, 
        SUM(o.Amount) AS TaxAmount  /*Уплаченная сумма налогов на текущий момент*/
    FROM `Operation` o 
    WHERE  1 = 1
    AND o.`GuildId` = @GuildId
    AND o.`CreateDate` >= @Data 
    AND o.`CreateDate` < ADDDATE(@Data, INTERVAL 1 MONTH)
    AND o.`Type` = 'Tax'
) tax ON tax.`GuildId` = ls.`GuildId`
LEFT JOIN (
    SELECT 
        o.`GuildId`, 
        sum(o.Amount) AS GamersBalance  /*Балланс игроков на складе гильдии*/
    FROM `Operation` o 
    WHERE  1 = 1
    AND o.`GuildId` = @GuildId
    AND o.`Type` = 'Other'
) gm ON gm.`GuildId` = ls.`GuildId`
";

        public Decimal ActiveLoansAmount { get; }
        public Decimal RepaymentLoansAmount { get; }
        public Decimal Balance { get; }
        public Decimal TaxAmount { get; }
        public Decimal GamersBalance { get; }
        protected GuildBalance() { }
    }
}
