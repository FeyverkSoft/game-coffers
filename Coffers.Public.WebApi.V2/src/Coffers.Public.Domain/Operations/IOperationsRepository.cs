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
        /// Возвращает информацию об операции
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<Operation> Get(Guid operationId, CancellationToken cancellationToken);
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
        Task<ICollection<Operation>> GetOperationWithDocIdAndType(Guid id, OperationType type, CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает штраф по его ID
        /// </summary>
        /// <param name="penaltyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Penalty> GetPenalty(Guid penaltyId, CancellationToken cancellationToken);
        /// <summary>
        /// Сохраняет штраф
        /// </summary>
        /// <param name="penalty"></param>
        /// <returns></returns>
        Task SavePenalty(Penalty penalty);
        /// <summary>
        /// Возвращает займ
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Loan> GetLoan(Guid loanId, CancellationToken cancellationToken);
        /// <summary>
        /// Сохраняет займ
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        Task SaveLoan(Loan loan);
    }
}
