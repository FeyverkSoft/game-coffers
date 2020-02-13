using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Entity;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public sealed class OperationCreator
    {
        private readonly IDocumentRepository _documentRepository;
        public OperationCreator(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }
        public async Task<Operation> Create(
            Guid id,
            Guid guildId,
            Guid userId,
            Guid? documentId,
            Decimal amount,
            OperationType type,
            String description,
            Guid? parentOperationId,
            CancellationToken cancellationToken)
        {
            if (Guid.Empty == id)
                throw new ArgumentException("OperationCreator: Value mustn't be empty", nameof(id));
            if (Guid.Empty == guildId)
                throw new ArgumentException("OperationCreator: Value mustn't be empty", nameof(guildId));
            if (Guid.Empty == userId)
                throw new ArgumentException("OperationCreator: Value mustn't be empty", nameof(userId));

            if (documentId != null)
            {
                switch (type)
                {
                    case OperationType.Loan:
                        if (!await _documentRepository.IsLoanExists(documentId.Value, userId, cancellationToken))
                            throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                        break;
                    case OperationType.Penalty:
                        if (!await _documentRepository.IsPenaltyExists(documentId.Value, userId, cancellationToken))
                            throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                        break;
                    case OperationType.Tax:
                        if (!await _documentRepository.IsTaxExists(documentId.Value, userId, cancellationToken))
                            throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                        break;
                }
            }

            return new Operation(
                  id,
                  guildId,
                  userId,
                  amount,
                  documentId,
                  type,
                  parentOperationId,
                  description?.Trim());
        }
    }
}
