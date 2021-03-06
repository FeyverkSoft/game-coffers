﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations.Entity;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public sealed class OperationCreator
    {
        private readonly DocumentValidator _validator;

        public OperationCreator(DocumentValidator validator)
        {
            _validator = validator;
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
            if (amount == 0m)
                throw new ArgumentException("OperationCreator: Value mustn't be zero", nameof(amount));

            if (type == OperationType.Tax && amount > 0)
                amount = -1.0m * amount;

            var operation = new Operation(
                id,
                guildId,
                userId,
                amount,
                parentOperationId,
                description?.Trim());

            if (documentId != null){
                await _validator.Validate(type, documentId.Value, userId, cancellationToken);
                operation.SetDocument(type, documentId.Value);
            }
            else{
                operation.SetOperationWithoutDocument(type);
            }

            return operation;
        }
    }
}