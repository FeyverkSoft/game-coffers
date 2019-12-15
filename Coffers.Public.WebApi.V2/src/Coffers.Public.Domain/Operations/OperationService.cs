using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Account;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Operations
{
    public sealed class OperationService
    {
        private readonly IOperationsRepository _oRepository;

        public OperationService(IOperationsRepository oRepository)
        {
            _oRepository = oRepository;
        }
    }
}
