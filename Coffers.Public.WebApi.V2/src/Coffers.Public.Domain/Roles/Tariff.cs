using System;

namespace Coffers.Public.Domain.Roles
{
    public class Tariff
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Decimal LoanTax { get; }
        public Decimal ExpiredLoanTax { get; }
        public String Tax { get; }

        public Tariff(decimal loanTax, decimal expiredLoanTax, string tax)
        => (LoanTax, ExpiredLoanTax, Tax)
        =  (loanTax, expiredLoanTax, tax);
    }
}
