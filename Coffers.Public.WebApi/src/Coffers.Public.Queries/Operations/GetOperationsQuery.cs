﻿using System;
using System.Collections.Generic;
using Coffers.Types.Account;
using Query.Core;

namespace Coffers.Public.Queries.Operations
{
    /// <summary>
    /// запрос на получение списка операций по документу
    /// </summary>
    public sealed class GetOperationsQuery : IQuery<ICollection<OperationView>>
    {
        public Guid UserId { get; set; }
        public Guid OperationId { get; set; }
        public OperationType Type { get; set; }
    }
}