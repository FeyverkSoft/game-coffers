﻿using System;
using System.Linq;
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

        public async Task PutLoan(Guid loanId, Guid toAccountId, Guid fromAccountId, Decimal loanAmount, Decimal loanTax)
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * loanAmount);
            toAccount.ChangeBalance(loanAmount + loanTax);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = loanId,
                Amount = loanAmount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Loan,
                Description = $"Системный перевод в пользу займа {loanId}",
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// красное сторно по займу
        /// Использовать крайне осторожно!!!!!
        /// Не тестировал
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public async Task CancelLoan(Guid loanId)
        {
            var loansOperations = await _oRepository.GetOperationWithDocIdAndType(loanId, OperationType.Loan);
            foreach (var op in loansOperations)
            {
                op.ToAccount.ChangeBalance(-1 * op.Amount);
                op.FromAccount.ChangeBalance(op.Amount);
                await _oRepository.Save(new Operation
                {
                    Id = Guid.NewGuid(),
                    DocumentId = loanId,
                    Amount = -1 * op.Amount,
                    OperationDate = DateTime.UtcNow,
                    Type = OperationType.Loan,
                    Description = $"Отмена операций по займу {loanId}",
                    FromAccount = op.ToAccount,
                    ToAccount = op.FromAccount,
                });
            }
        }

        /// <summary>
        /// Производит операцию иного типа над двумя счетами
        /// </summary>
        /// <returns></returns>
        public async Task AddOtherOperation(Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Other,
                Description = description,
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// Производит операцию иного типа над одним счётом
        /// То что не указан второй счёт это норма. Так как в этом случае считаем что второй счёт некий забалансовый активно-пассивный счёт
        /// </summary>
        /// <returns></returns>
        public async Task AddOtherOperation(Guid toAccountId, Decimal amount, String description = "")
        {
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Other,
                Description = description,
                ToAccount = toAccount,
            });
        }

        public async Task AddRewardOperation(Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Reward,
                Description = description,
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        public async Task AddSalaryOperation(Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Salary,
                Description = description,
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        public async Task AddTaxOperation(Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Tax,
                Description = description,
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        public async Task AddPenaltyOperation(Guid gamerAccountId, Guid guildAccountId,
            Guid penaltyId, Decimal amount, String description)
        {
            var gamerAccount = await _oRepository.GetAccount(gamerAccountId, default);
            var guildAccount = await _oRepository.GetAccount(guildAccountId, default);
            var penalty = await _oRepository.GetPenalty(penaltyId, default);
            var penaltyOpSum = (await _oRepository.GetOperationWithDocIdAndType(penaltyId, OperationType.Penalty)).Sum(_ => _.Amount);
            var overSum = penaltyOpSum + amount - penalty.Amount;
            gamerAccount.ChangeBalance(overSum >= 0 ? overSum : -1 * amount);
            guildAccount.ChangeBalance(overSum >= 0 ? 0 : amount);

            await _oRepository.Save(new Operation
            {
                Id = Guid.NewGuid(),
                DocumentId = penaltyId,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Penalty,
                Description = description,
                FromAccount = gamerAccount,
                ToAccount = guildAccount,
            }).ContinueWith(async c =>
            {
                if (overSum >= 0)
                {
                    c.Wait();
                    penalty.PenaltyStatus = PenaltyStatus.InActive;
                    await _oRepository.SavePenalty(penalty);
                }
            });
        }

        public async Task AddLoanOperation(Guid fromGamerAccountId, Guid guildAccAccountId, Guid loanId,
            Decimal amount, String description)
        {
            throw new NotImplementedException();
        }
    }
}
