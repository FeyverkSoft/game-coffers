using System;

namespace Coffers.LoanWorker.Domain
{
    /// <summary>
    /// Тариф под который был взят займ
    /// </summary>
    public class Tariff
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Стоимость займа в день
        /// </summary>
        public Decimal LoanTax { get; set; }
        /// <summary>
        /// Стоимость просрочки займа в день
        /// </summary>
        public Decimal ExpiredLoanTax { get; set; }
    }
}
