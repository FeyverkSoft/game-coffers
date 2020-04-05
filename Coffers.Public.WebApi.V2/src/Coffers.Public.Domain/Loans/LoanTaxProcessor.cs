namespace Coffers.Public.Domain.Loans
{
    public sealed class LoanTaxProcessor
    {
        public void ProcessExpireLoan(Loan loan)
        {
            // если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.IsFreeTax)
                return;

            if (!loan.IsExpired)
                return;

            if (loan.ExpireLifetime <= 0)
                return;

            // если просрочка больше 2х месяцев (60 дней), то такой займ больше не облогаем налогом
            if (loan.ExpireLifetime > 60)
                return;

            loan.SetPenaltyAmount(loan.ExpireLifetime * loan.GetExpireTaxAmountPerDay());
        }

        public void ProcessLoanTax(Loan loan)
        {
            // если займ уже погашен, или стух, то пропускаем
            if (!loan.IsActive || loan.IsExpired)
                return;

            // если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.IsFreeTax)
                return;

            if (loan.Lifetime <= 0)
                return;

            loan.SetTaxAmount(loan.Lifetime * loan.GetTaxAmountPerDay());
        }
    }
}
