using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public interface IOperationsRepository
    {
        /// <summary>
        /// Возвращает информацию о счёте по его ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Account> GetAccount(Guid accountId, CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет операциюв бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task Save(Operation operation);
        /// <summary>
        /// Сохраняет операции в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task Save(ICollection<Operation> operation);

        /// <summary>
        /// Получить информацию об операциях по документу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<ICollection<Operation>> GetOperationWithDocIdAndType(Guid id, OperationType type);

        Task<Penalty> GetPenalty(Guid penaltyId, CancellationToken cancellationToken);
        Task SavePenalty(Penalty penalty);
        Task<Loan> GetLoan(Guid loanId, CancellationToken cancellationToken);
        Task SaveLoan(Loan loan);
    }
}
