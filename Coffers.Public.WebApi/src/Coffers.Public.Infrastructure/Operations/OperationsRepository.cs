using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations;
using Coffers.Types.Account;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Operations
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly OperationsDbContext _context;

        public OperationsRepository(OperationsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает информацию об операции
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<Operation> Get(Guid operationId, CancellationToken cancellationToken)
        {
            return await _context.Operations.FirstOrDefaultAsync(_ => _.Id == operationId, cancellationToken);
        }

        /// <summary>
        /// Возвращает информацию о счёте по его ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<Account> GetAccount(Guid accountId, CancellationToken cancellationToken)
        {
            return await _context.Accounts.FirstOrDefaultAsync(_ => _.Id == accountId, cancellationToken);
        }

        /// <summary>
        /// Сохраняет операциюв бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task Save(Operation operation)
        {
            var entry = _context.Entry(operation);
            if (entry.State == EntityState.Detached)
                _context.Operations.Add(operation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Сохраняет операции в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task Save(ICollection<Operation> operation)
        {
            _context.Operations.AddRange(operation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить информацию об операциях по документу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ICollection<Operation>> GetOperationWithDocIdAndType(Guid id, OperationType type)
        {
            return await _context.Operations
                .Include(_ => _.FromAccount)
                .Include(_ => _.ToAccount)
                .Where(_ => _.DocumentId == id && _.Type == type)
                .ToListAsync();
        }

        public async Task<Penalty> GetPenalty(Guid penaltyId, CancellationToken cancellationToken)
        {
            return await _context.Penalties
                .Where(_ => _.Id == penaltyId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SavePenalty(Penalty penalty)
        {
            var entry = _context.Entry(penalty);
            if (entry.State == EntityState.Detached)
                _context.Penalties.Add(penalty);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan> GetLoan(Guid loanId, CancellationToken cancellationToken)
        {
            return await _context.Loans
                .Include(_ => _.Account)
                .Where(_ => _.Id == loanId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SaveLoan(Loan loan)
        {
            var entry = _context.Entry(loan);
            if (entry.State == EntityState.Detached)
                _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }
    }
}
