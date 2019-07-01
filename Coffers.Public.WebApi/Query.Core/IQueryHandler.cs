using System.Threading;
using System.Threading.Tasks;

namespace Query.Core
{
    /// <summary>
    /// Интерфейс обработчика запросов
    /// </summary>
    public interface IQueryHandler
    {
    }

    /// <summary>
    /// Интерфейс обработчика запросов типизированный
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IQueryHandler<TQuery, TResult> : IQueryHandler where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}