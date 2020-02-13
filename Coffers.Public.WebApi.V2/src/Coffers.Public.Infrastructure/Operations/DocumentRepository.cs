using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations;
using Microsoft.EntityFrameworkCore;

namespace Coffers.Public.Infrastructure.Operations
{
    public sealed class DocumentRepository : IDocumentRepository
    {
        private readonly OperationDbContext _context;
        public DocumentRepository(OperationDbContext context)
        {
            _context = context;
        }

        public async Task<Boolean> IsLoanExists(Guid documentId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Loans.AnyAsync(_ => _.Id == documentId && _.UserId == userId, cancellationToken);
        }

        public async Task<Boolean> IsPenaltyExists(Guid documentId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Penalties.AnyAsync(_ => _.Id == documentId && _.UserId == userId, cancellationToken);
        }

        public async Task<Boolean> IsTaxExists(Guid documentId, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Taxes.AnyAsync(_ => _.Id == documentId && _.UserId == userId, cancellationToken);
        }
    }
}
