using System;
using System.Collections.Generic;
using System.Linq;
using Query.Core.Helper;

namespace Query.Core
{

    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly Dictionary<Type, Type> _handlers = new Dictionary<Type, Type>();

        public IEnumerable<Type> RegisteredQueries => _handlers.Keys;
        public IEnumerable<Type> RegisteredHandlers => _handlers.Values;

        public QueryHandlerRegistry Register<H>() where H : IQueryHandler
        {
            var supportedQueryTypes = typeof(H).GetGenericInterfaces(typeof(IQueryHandler<,>));

            if (_handlers.Keys.Any(registeredType => supportedQueryTypes.Contains(registeredType)))
                throw new ArgumentException("The query handled by the received handler already has a registered handler.");

            foreach (var key in supportedQueryTypes)
                _handlers.Add(key, typeof(H));

            return this;
        }

        public Type HandlerFor(Type queryType)
        {
            if (!_handlers.TryGetValue(queryType, out var type))
                throw new KeyNotFoundException("Not found Hanlder");
            return type;
        }
    }
}
