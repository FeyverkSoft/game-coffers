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
        Task<IEnumerable<Loan>> GetAllUnprocessedExpiredLoan(CancellationToken cancellationToken);
        Task<IEnumerable<Loan>> GetExpiredLoan(CancellationToken cancellationToken);
        Task<IEnumerable<Loan>> GetActiveLoan(CancellationToken cancellationToken);
    }
}
