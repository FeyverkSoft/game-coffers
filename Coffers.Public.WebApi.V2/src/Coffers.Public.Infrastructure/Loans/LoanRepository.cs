using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Loans;
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

        public async Task Save(Loan loan)
        {
            var entry = _context.Entry(loan);
            if (entry.State == EntityState.Detached)
                _context.Loans.Add(loan);

            await _context.SaveChangesAsync();
        }
    }
}
