using System;

namespace Coffers.DB.Migrations.Entities
{
    internal class Tariff
    {
        public Guid Id { get; set; }

        public Decimal LoanTax { get; set; }

        public Decimal ExpiredLoanTax { get; set; }

        public String Tax { get; set; }
    }
}
