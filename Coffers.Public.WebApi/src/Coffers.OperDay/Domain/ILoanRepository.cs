using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.LoanWorker.Domain
{
    public interface ILoanRepository
    {

        /// <summary>
        /// Возвращает список всех стухших займов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<Loan>> GetExpiredLoans(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список всех активных займов
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<Loan>> GetActiveLoans(CancellationToken cancellationToken);

        /// <summary>
        /// Сохраняет информацию по займу
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        Task SaveLoan(Loan loan);
    }
}