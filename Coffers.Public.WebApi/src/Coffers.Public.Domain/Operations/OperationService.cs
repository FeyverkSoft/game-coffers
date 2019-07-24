using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Gamers;
using Coffers.Public.Domain.Guilds;
using Coffers.Types.Account;

namespace Coffers.Public.Domain.Operations
{
    public sealed class OperationService
    {
        private readonly IOperatioRepository _oRepository;
        private readonly IGamerRepository _gamerRepository;
        private readonly IGuildRepository _guildRepository;

        public OperationService(
            IOperatioRepository oRepository,
            IGamerRepository gmRepository,
            IGuildRepository guildRepository)
        {
            _oRepository = oRepository;
            _gamerRepository = gmRepository;
            _guildRepository = guildRepository;
        }

        public async Task PutLoan(Guid loanId, Guid toAccountId, Guid fromAccountId, Decimal loanAmount, Decimal loanTax)
        {
            var fromAccount = _oRepository.GetAccount(fromAccountId);
            var toAccount = _oRepository.GetAccount(toAccountId);
            await _oRepository.Add(new Operation
            {
                Id = Guid.NewGuid(),
                Amount = loanAmount,
                CreateDate = DateTime.UtcNow,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Loan,
                Description = "Системный перевод в пользу займа",
                FromAccountId = fromAccount,
                ToAccountId = toAccount,
            });
        }

        public async Task CancelLoan(Guid bindingId)
        {
            throw new NotImplementedException();
        }
    }
}
