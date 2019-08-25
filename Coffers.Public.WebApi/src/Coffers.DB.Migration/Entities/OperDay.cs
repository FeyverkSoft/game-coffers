using System;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class OperDay
    {
        /// <summary>
        /// Гильдия
        /// </summary>
        public Guild Guild { get; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; }

        public Decimal Balance { get; }

        public Decimal Tax { get; }

        public Decimal UsersBalance { get; }

        public Decimal LoansBalance { get; }
    }
}
