using System;
using System.Collections.Generic;

namespace Coffers.Public.Queries
{
    public sealed class Page<T> where T : class
    {
        public Int32 Total { get; set; }

        public Int32 Offset { get; set; }

        public Int32 Limit { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}