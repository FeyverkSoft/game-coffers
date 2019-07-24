using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Operations;
using Query.Core;

namespace Coffers.Public.Infrastructure.Operations
{
    public sealed class OperationsQueryHandler : IQueryHandler<GetOperationsQuery, ICollection<OperationView>>
    {
        private readonly OperationsDbContext _context;

        public OperationsQueryHandler(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<OperationView>> Handle(GetOperationsQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
