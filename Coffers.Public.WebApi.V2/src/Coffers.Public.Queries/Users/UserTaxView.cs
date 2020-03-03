using System;
using System.Collections.Generic;

namespace Coffers.Public.Queries.Users
{
  public sealed class UserTaxView
    {
        public Guid UserId { get; }
        public Decimal TaxAmount { get; }
        public IEnumerable<Decimal> TaxTariff { get; }

        public UserTaxView(Guid userId, Decimal taxAmount, IEnumerable<Decimal> taxTariff)
            => (UserId, TaxAmount, TaxTariff)
                = (userId, taxAmount, taxTariff);
    }
}
