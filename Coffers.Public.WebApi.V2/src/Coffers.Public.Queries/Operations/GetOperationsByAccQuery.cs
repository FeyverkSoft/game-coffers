﻿using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Operations
{
    /// <summary>
    /// запрос на получение списка операций по счёту
    /// </summary>
    public sealed class GetOperationsByAccQuery : IQuery<ICollection<OperationView>>
    {
        public Guid AccountId { get; set; }
        public DateTime DateMonth { get; set; }
    }
}
