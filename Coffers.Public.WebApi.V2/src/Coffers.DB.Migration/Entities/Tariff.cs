using System;

namespace Coffers.DB.Migrations.Entities
{
    internal class Tariff
    {
        public Guid Id { get; }

        public Decimal LoanTax { get; }

        public Decimal ExpiredLoanTax { get; }

        public String Tax { get; }
    }
}
