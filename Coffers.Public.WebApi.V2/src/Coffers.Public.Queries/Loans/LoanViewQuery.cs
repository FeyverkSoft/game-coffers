using System;
using Query.Core;

namespace Coffers.Public.Queries.Loans
{
    public sealed class LoanViewQuery : IQuery<LoanView>
    {
        /// <summary>
        /// Loan id
        /// </summary>
        public Guid LoanId { get; }

        public LoanViewQuery(Guid loanId)
            => (LoanId)
                = (loanId);
    }
}