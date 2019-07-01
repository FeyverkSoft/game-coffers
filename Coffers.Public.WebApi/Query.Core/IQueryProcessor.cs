using System.Threading;
using System.Threading.Tasks;

namespace Query.Core
{
    /// <summary>
    /// Интерфейс обработчика запросов
    /// </summary>
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
    }

}