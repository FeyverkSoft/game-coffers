using System.Threading.Tasks;

namespace Coffers.LoanWorker.Domain
{
    public interface IOperationRepository
    {
        /// <summary>
        /// Сохраняет операцию в бд
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task Save(Operation operation);
    }
}
