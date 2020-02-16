using System;
using Coffers.Types.Account;

namespace Coffers.Public.WebApi.Models.Operation
{
    public class AddOperationDocumentBinding
    {
        public Guid DocumentId { get; set; }
        public OperationType Type { get; set; }
    }
}
