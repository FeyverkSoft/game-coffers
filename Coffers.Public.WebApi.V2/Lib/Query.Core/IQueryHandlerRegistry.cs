using System;
using System.Collections.Generic;

namespace Query.Core
{
    public interface IQueryHandlerRegistry
    {
        IEnumerable<Type> RegisteredQueries { get; }

        Type HandlerFor(Type type);
    }
}