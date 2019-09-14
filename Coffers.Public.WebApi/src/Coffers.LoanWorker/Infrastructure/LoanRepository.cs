using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.LoanWorker.Domain;
using Coffers.LoanWorker.Infrastructure;

using Microsoft.EntityFrameworkCore;

using Coffers.Types.Gamer;
using Coffers.Helpers;

namespace Coffers.LoanWorker
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanWorkerDbContext _context;

        public LoanRepository(LoanWorkerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех стухших займов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Loan>> GetExpiredLoans(CancellationToken cancellationToken)
        {
            var truncedDate = DateTime.UtcNow.Trunc(DateTruncType.Day);
            return await _context.Loans
                .Include(_ => _.Account)
                .Include(_ => _.Tariff)
                .Where(_ => (_.LoanStatus == LoanStatus.Active || _.LoanStatus == LoanStatus.Expired) && _.ExpiredDate < truncedDate)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Возвращает список всех активных займов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Loan>> GetActiveLoans(CancellationToken cancellationToken)
        {
            var truncedDate = DateTime.UtcNow.Trunc(DateTruncType.Day);
            return await _context.Loans
                .Include(_ => _.Account)
                .Include(_ => _.Tariff)
                .Where(_ => (_.LoanStatus == LoanStatus.Active) && truncedDate > _.ExpiredDate)
                .ToListAsync(cancellationToken);
        }


        /// <summary>
        /// Сохраняет информацию по займу
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task SaveLoan(Loan loan)
        {
            var entry = _context.Entry(loan);
            if (entry.State == EntityState.Detached)
                _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }
    }
}
