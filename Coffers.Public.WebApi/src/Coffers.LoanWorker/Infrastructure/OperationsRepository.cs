using System.Threading.Tasks;
using Coffers.LoanWorker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Coffers.LoanWorker.Infrastructure
{
    public class OperationsRepository : IOperationRepository
    {
        private readonly LoanWorkerDbContext _context;

        public OperationsRepository(LoanWorkerDbContext context)
        {
            _context = context;
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
    }
}
