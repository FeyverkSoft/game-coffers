using System;
using Query.Core;

namespace Coffers.Public.Queries
{
    public abstract class PageQuery<T> : IQuery<Page<T>> where T : class
    {
        protected PageQuery(Int32 offset, Int32 limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public Int32 Limit { get; }

        public Int32 Offset { get; }
    }
}