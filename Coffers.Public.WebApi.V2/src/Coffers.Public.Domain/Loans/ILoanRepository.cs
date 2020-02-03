using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public interface ILoanRepository
    {
        Task<Loan> Get(Guid id, CancellationToken cancellationToken);

        Task Save(Loan loan);
        Task<IEnumerable<Loan>> GetAllUnprocessedExpiredLoan();
        Task<IEnumerable<Loan>> GetExpiredLoan();
        Task<IEnumerable<Loan>> GetActiveLoan();
    }
}
