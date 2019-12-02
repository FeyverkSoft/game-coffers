using System;
using System.Threading;
using System.Threading.Tasks;

namespace Query.Core
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IQueryHandlerRegistry _registry;

        public QueryProcessor(IServiceProvider serviceProvider, IQueryHandlerRegistry registry)
        {
            _serviceProvider = serviceProvider;
            _registry = registry;
        }

        public async Task<TResult> Process<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>
        {
            var handlerType = _registry.HandlerFor(query.GetType());
            var queryHandler = (IQueryHandler<TQuery, TResult>)_serviceProvider.GetService(handlerType);
            return await queryHandler.Handle(query, cancellationToken);
        }
    }
}