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
    }
}
