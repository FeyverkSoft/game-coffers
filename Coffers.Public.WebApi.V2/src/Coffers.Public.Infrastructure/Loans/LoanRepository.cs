using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans;
using Coffers.Public.Domain.Loans.Entity;
using Coffers.Types.Gamer;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Loans
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDbContext _context;

        public LoanRepository(LoanDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Loans
                .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Loan>> GetActiveLoan(CancellationToken cancellationToken)
        {
            return await _context.Loans
                .Include(_ => _.Tariff)
                .Where(_ => _.ExpiredDate > DateTime.UtcNow &&
                            _.LoanStatus == LoanStatus.Active)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Loan>> GetAllUnprocessedExpiredLoan(CancellationToken cancellationToken)
        {
            return await _context.Loans
                .Where(_ => _.ExpiredDate <= DateTime.UtcNow &&
                            _.LoanStatus == LoanStatus.Active)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Loan>> GetExpiredLoan(CancellationToken cancellationToken)
        {
            return await _context.Loans
                .Include(_ => _.Tariff)
                .Where(_ => _.LoanStatus == LoanStatus.Expired)
                .ToListAsync(cancellationToken);
        }

        public async Task Save(Loan loan)
        {
            var entry = _context.Entry(loan);
            if (entry.State == EntityState.Detached)
                _context.Loans.Add(loan);

            await _context.SaveChangesAsync();
        }
    }
}
