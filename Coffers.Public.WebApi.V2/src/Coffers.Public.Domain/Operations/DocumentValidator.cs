using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public sealed class DocumentValidator
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentValidator(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Boolean> Validate(OperationType type, Guid documentId, Guid userId, CancellationToken cancellationToken)
        {
            switch (type)
            {
                case OperationType.Loan:
                    if (!await _documentRepository.IsLoanExists(documentId, userId, cancellationToken))
                        throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                    break;
                case OperationType.Penalty:
                    if (!await _documentRepository.IsPenaltyExists(documentId, userId, cancellationToken))
                        throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                    break;
                case OperationType.Tax:
                    if (!await _documentRepository.IsTaxExists(documentId, userId, cancellationToken))
                        throw new DocumentNotFoundException($"Document: {documentId} of type {type} not found");
                    break;
            }

            return true;
        }
    }
}
