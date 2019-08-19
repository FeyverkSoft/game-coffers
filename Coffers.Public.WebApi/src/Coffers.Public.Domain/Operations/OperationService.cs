using System;
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
            var loan = await _oRepository.GetLoan(loanId, default);
            foreach (var op in loansOperations)
            {
                op.ToAccount.ChangeBalance(-1 * op.Amount);
                op.FromAccount.ChangeBalance(op.Amount);
                await _oRepository.Save(new Operation
                {
                    Id = Guid.NewGuid(),
                    DocumentId = loanId,
                    Amount = op.Amount,
                    OperationDate = DateTime.UtcNow,
                    Type = OperationType.Loan,
                    Description = $"Отмена операций по займу {loan.Description} - {loanId}",
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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Other || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Other || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Other || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.InternalOutput || operation.Amount != amount))
                throw new Exception("Operation already exists");
            if (operation != null)
                return;

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
        /// Эмиссия бабок на склад, как итог продажи придметов со склада ги
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toAccountId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddSellOperation(Guid id, Guid toAccountId, decimal amount, string description)
        {
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Other || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

            var toAccount = await _oRepository.GetAccount(toAccountId, default);
            toAccount.ChangeBalance(amount);
            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Sell,
                Description = description,
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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Tax || operation.Amount != amount))
                throw new Exception("Operation already exists");
            if (operation != null)
                return;

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
        /// <param name="guildAccountId"></param>
        /// <param name="penaltyId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddPenaltyOperation(Guid id, Guid guildAccountId,
            Guid penaltyId, Decimal amount, String description)
        {
            var operation = await _oRepository.Get(id, default);
            if (operation != null)
                throw new OperationException("Operation already exists");

            var penalty = await _oRepository.GetPenalty(penaltyId, default);

            var gamerAccount = penalty.Gamer.DefaultAccount;
            var guildAccount = await _oRepository.GetAccount(guildAccountId, default);

            var penaltyOpSum = (await _oRepository.GetOperationWithDocIdAndType(penaltyId, OperationType.Penalty))
                .Sum(_ => _.Amount);
            var overSum = penaltyOpSum + amount - penalty.Amount;

            gamerAccount.ChangeBalance(-1 * (amount - (overSum > 0 ? overSum : 0)));
            guildAccount.ChangeBalance(amount - (overSum > 0 ? overSum : 0));

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
                    penalty.SetStatus(PenaltyStatus.InActive);
                    await _oRepository.SavePenalty(penalty);
                }
            });
        }

        /// <summary>
        /// Добавляет новую операцию в пользу займа
        /// </summary>
        /// <param name="guildAccAccountId"></param>
        /// <param name="loanId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task AddLoanOperation(Guid id, Guid guildAccAccountId, Guid loanId,
            Decimal amount, String description)
        {
            var operation = await _oRepository.Get(id, default);
            if (operation != null)
                throw new Exception("Operation already exists");

            var operations = new List<Operation>();
            var loan = await _oRepository.GetLoan(loanId, default);
            
            var gamerAccount = loan.Gamer.DefaultAccount;
            var guildAccount = await _oRepository.GetAccount(guildAccAccountId, default);

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
                Type = OperationType.Loan,
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
                    loan.SetStatus(LoanStatus.Paid);
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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Emission || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

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
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.InternalEmission || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

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

        /// <summary>
        /// Производит операцию обмена голдой между разными чарами через склад
        /// </summary>
        /// <returns></returns>
        public async Task DoExchangeOperation(Guid id, Guid accountId, Decimal amount, String description = "")
        {
            var operation = await _oRepository.Get(id, default);
            if (operation != null && (operation.Type != OperationType.Other || operation.Amount != amount))
                throw new OperationException("Operation already exists");
            if (operation != null)
                return;

            var account = await _oRepository.GetAccount(accountId, default);

            await _oRepository.Save(new Operation
            {
                Id = id,
                DocumentId = null,
                Amount = amount,
                OperationDate = DateTime.UtcNow,
                Type = OperationType.Exchange,
                Description = description,
                FromAccount = account,
                ToAccount = account,
            });
        }
    }
}
