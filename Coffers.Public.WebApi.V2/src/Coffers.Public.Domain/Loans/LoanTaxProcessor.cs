using System;
using Coffers.Helpers;

namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanTaxProcessor
    {
        internal void ProcessExpireLoan(Loan loan)
        {
            // если займ уже погашен, или время не наступило, то пропускаем
            // такого быть не должно... но...
            if (!loan.IsActive || loan.ExpiredDate > DateTime.UtcNow)
                return;

            // если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.Tariff.ExpiredLoanTax <= 0)
                return;

            // Вычисляем сколько дней он уже просрочен
            var days = (Decimal)Math.Floor((DateTime.UtcNow.Trunc(DateTruncType.Day) - loan.ExpiredDate.Trunc(DateTruncType.Day)).TotalDays);

            if (days <= 0)
                return;
            // если просрочка больше 2х месяцев (60 дней), то такой займ больше не облогаем налогом
            if (days > 60)
                return;

            // Сума просрочки в день
            var amountPerDay = loan.Amount * (loan.Tariff.ExpiredLoanTax / 100);

            loan.SetPenaltyAmount(days * amountPerDay);
        }

        internal void ProcessLoanTax(Loan loan)
        {
            // если займ уже погашен, или стух, то пропускаем
            if (!loan.IsActive || loan.ExpiredDate < DateTime.UtcNow)
                return;

            // если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.Tariff.LoanTax <= 0)
                return;

            // Вычисляем сколько дней он уже существует
            var days = (Decimal)Math.Floor((DateTime.UtcNow.Trunc(DateTruncType.Day) - loan.CreateDate.Trunc(DateTruncType.Day)).TotalDays);

            if (days <= 0)
                return;

            // Сума налога в день
            var amountPerDay = loan.Amount * (loan.Tariff.LoanTax / 100);

            loan.SetTaxAmount(days * amountPerDay);
        }
    }
}
