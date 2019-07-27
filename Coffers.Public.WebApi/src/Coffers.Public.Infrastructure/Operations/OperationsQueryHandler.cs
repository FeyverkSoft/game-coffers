using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Operations;
using Microsoft.EntityFrameworkCore;
using Query.Core;

namespace Coffers.Public.Infrastructure.Operations
{
    public sealed class OperationsQueryHandler : IQueryHandler<GetOperationsQuery, ICollection<OperationView>>,
        IQueryHandler<GetOperationsByAccQuery, ICollection<OperationView>>
    {
        private readonly OperationsDbContext _context;

        public OperationsQueryHandler(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<OperationView>> Handle(GetOperationsQuery query, CancellationToken cancellationToken)
        {
            var q = _context.Operations.AsNoTracking()
                .Where(o => o.DocumentId == query.DocumentId && o.Type == query.Type);

            return await q
                .OrderBy(o => o.OperationDate)
                .Select(o => new OperationView
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    DocumentId = o.DocumentId,
                    Type = o.Type,
                    Description = o.Description,
                    CreateDate = o.OperationDate
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ICollection<OperationView>> Handle(GetOperationsByAccQuery query,
            CancellationToken cancellationToken)
        {
            var q = _context.Operations.AsNoTracking()
                .Where(_=>_.CreateDate>= query.DateFrom);

            var result = new List<OperationView>();
            var to = await q
                   .Where(o => o.ToAccount != null && o.ToAccount.Id == query.AccountId)
                   .OrderBy(o => o.OperationDate)
                   .Select(o => new OperationView
                   {
                       Id = o.Id,
                       Amount = o.Amount,
                       DocumentId = o.DocumentId,
                       Type = o.Type,
                       Description = o.Description,
                       CreateDate = o.OperationDate
                   })
                   .ToListAsync(cancellationToken);

            var from = await q
                .Where(o => o.FromAccount != null && o.FromAccount.Id == query.AccountId)
                .OrderBy(o => o.OperationDate)
                .Select(o => new OperationView
                {
                    Id = o.Id,
                    Amount = o.Amount,
                    DocumentId = o.DocumentId,
                    Type = o.Type,
                    Description = o.Description,
                    CreateDate = o.OperationDate
                })
                .ToListAsync(cancellationToken);

            result.AddRange(to);
            result.AddRange(from);
            return result;
        }
    }
}
