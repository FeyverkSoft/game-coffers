using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Operations
{
    public sealed class OperationListView
    {
        public Guid Id { get; }
        public Decimal Amount { get; }
        public DateTime CreateDate { get; }
        public String Description { get; }
        public OperationType Type { get; }
        public Guid DocumentId { get; }
        public Decimal DocumentAmount { get; }
        public String DocumentDescription { get; }
        public Guid UserId { get; }
        public String UserName { get; }

        public OperationListView(
            in Guid id,
            in Decimal amount,
            in DateTime date,
            in String description,
            in OperationType type,
            in Guid documentId,
            in Decimal documentAmount,
            in String documentDescription,
            in Guid userId,
            in String userName)
            => (Id, Amount, CreateDate, Description, Type, DocumentId, DocumentAmount, DocumentDescription, UserId,
                    UserName) =
                (id, amount, date, description, type, documentId, documentAmount, documentDescription, userId,
                    userName);
    }
}
