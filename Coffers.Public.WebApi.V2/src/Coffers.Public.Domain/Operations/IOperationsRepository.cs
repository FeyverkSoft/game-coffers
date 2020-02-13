using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Entity;

namespace Coffers.Public.Domain.Operations
{
    public interface IOperationsRepository
    {
        /// <summary>
        /// Возвращает информацию об операции
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<Operation> Get(Guid operationId, CancellationToken cancellationToken);
       

        /// <summary>
        /// Сохраняет операциюв бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task Save(Operation operation);
    }
}
