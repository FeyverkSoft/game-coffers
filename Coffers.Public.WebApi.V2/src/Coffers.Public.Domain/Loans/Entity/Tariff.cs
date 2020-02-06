using System;

namespace Coffers.Public.Domain.Loans
{
    public class Tariff
    {
        public Guid Id { get; set; }

        public Decimal LoanTax { get; set; }

        public Decimal ExpiredLoanTax { get; set; }
    }
}
