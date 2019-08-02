﻿using System;
using System.Collections.Generic;
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
        public async Task AddOtherOperation(Guid id, Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
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
        public async Task AddOtherOperation(Guid id, Guid toAccountId, Decimal amount, String description = "")
        {
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Other,
                Description = description,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// Операция вывод средств из гильдии во внешнюю систему в пользу игрока
        /// Через зачисление на гильдиский счёт игрока
        /// </summary>
        /// <param name="fromAccountId"></param>
        /// <param name="toAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task DoOutputOperation(Guid id, Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(0);
            var operations = new[] {
                new Operation
                {
                    Id = id,
                    DocumentId = null,
                    Amount = amount,
                    OperationDate = DateTime.UtcNow,
                    Type = OperationType.Other,
                    Description = $"Промежуточный перевод на счёт игрока со счёта гильдии, для последующего вывода; {description}",
                    FromAccount = fromAccount,
                    ToAccount = toAccount,
                },
                new Operation
                {
                    Id = Guid.NewGuid(),
                    DocumentId = null,
                    Amount = amount,
                    OperationDate = DateTime.UtcNow,
                    Type = OperationType.Output,
                    Description = description,
                    FromAccount = toAccount
                },
            };
            await _oRepository.Save(operations);
        }

        /// <summary>
        /// Операция вывод средств из гильдии в пользу счёта игрока без эмиссии во внешнюю систему
        /// </summary>
        /// <param name="fromAccountId"></param>
        /// <param name="toAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task DoInternalOutputOperation(Guid id, Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.InternalOutput,
                Description = $"Перевод на счёт игрока со счёта гильдии; {description}",
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// Добавляет новую операцию уплаты налога
        /// </summary>
        /// <param name="fromAccountId"></param>
        /// <param name="toAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddTaxOperation(Guid id, Guid fromAccountId, Guid toAccountId, Decimal amount, String description = "")
        {
            var fromAccount = await _oRepository.GetAccount(fromAccountId, default);
            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            fromAccount.ChangeBalance(-1 * amount);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Tax,
                Description = description,
                FromAccount = fromAccount,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// Добавляет новую операцию погашения займа
        /// </summary>
        /// <param name="gamerAccountId"></param>
        /// <param name="guildAccountId"></param>
        /// <param name="penaltyId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddPenaltyOperation(Guid id, Guid gamerAccountId, Guid guildAccountId,
            Guid penaltyId, Decimal amount, String description)
        {
            var gamerAccount = await _oRepository.GetAccount(gamerAccountId, default);
            var guildAccount = await _oRepository.GetAccount(guildAccountId, default);
            var penalty = await _oRepository.GetPenalty(penaltyId, default);
            var penaltyOpSum = (await _oRepository.GetOperationWithDocIdAndType(penaltyId, OperationType.Penalty))
                .Sum(_ => _.Amount);
            var overSum = penaltyOpSum + amount - penalty.Amount;

            gamerAccount.ChangeBalance(amount - (overSum > 0 ? overSum : 0));
            guildAccount.ChangeBalance(-1 * (amount - (overSum > 0 ? overSum : 0)));

            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = penaltyId,
                Amount = amount - (overSum > 0 ? overSum : 0),
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

        /// <summary>
        /// Добавляет новую операцию в пользу займа
        /// </summary>
        /// <param name="gamerAccountId"></param>
        /// <param name="guildAccAccountId"></param>
        /// <param name="loanId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddLoanOperation(Guid id, Guid gamerAccountId, Guid guildAccAccountId, Guid loanId,
            Decimal amount, String description)
        {
            var operations = new List<Operation>();
            var gamerAccount = await _oRepository.GetAccount(gamerAccountId, default);
            var guildAccount = await _oRepository.GetAccount(guildAccAccountId, default);
            var loan = await _oRepository.GetLoan(loanId, default);

            var lA = 0m;
            if (loan.Account.Balance <= 0)
                lA = 0;
            else
            {
                if (loan.Account.Balance >= amount)
                    lA = amount;
                if (loan.Account.Balance < amount)
                    lA = loan.Account.Balance;
            }

            loan.Account.ChangeBalance(-1 * lA);

            if (amount - lA > 0)
            {
                operations.Add(new Operation
                {
                    Id = Guid.NewGuid(),
                    Amount = amount - lA,
                    OperationDate = DateTime.UtcNow,
                    Type = OperationType.Other,
                    Description = $"Сдача с погашения займа#{loanId}",
                    ToAccount = gamerAccount,
                });
                gamerAccount.ChangeBalance(amount - lA);
            }

            guildAccount.ChangeBalance(lA);
            operations.Add(new Operation
            {
                Id = id,
                DocumentId = loanId,
                Amount = lA,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Penalty,
                Description = description,
                FromAccount = loan.Account,
                ToAccount = guildAccount,
            });

            await _oRepository.Save(operations)
                .ContinueWith(async c =>
            {
                if (loan.Account.Balance <= 0)
                {
                    c.Wait();
                    loan.LoanStatus = LoanStatus.Paid;
                    await _oRepository.SaveLoan(loan);
                }
            });
        }

        /// <summary>
        /// Выполняет операцию эмиссии средств на счёт гильдии
        /// </summary>
        /// <param name="guildAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task EmissionOperation(Guid id, Guid guildAccountId, Decimal amount, String description)
        {
            var toAccount = await _oRepository.GetAccount(guildAccountId, default);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Emission,
                Description = description,
                ToAccount = toAccount,
            });
        }

        /// <summary>
        /// Перевод игроком со своего счёта голды, в пользу счёта гильдии
        /// По иной причине, отличной от займа итд
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gamerAccountId"></param>
        /// <param name="guildAccAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task DoInternalEmissionOperation(Guid id, Guid gamerAccountId, Guid guildAccAccountId,
            Decimal amount, String description)
        {
            var gamerAccount = await _oRepository.GetAccount(gamerAccountId, default);
            var guildAccount = await _oRepository.GetAccount(guildAccAccountId, default);
            gamerAccount.ChangeBalance(-1 * amount);
            guildAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.InternalEmission,
                Description = description,
                FromAccount = gamerAccount,
                ToAccount = guildAccount,
            });
        }
    }
}
