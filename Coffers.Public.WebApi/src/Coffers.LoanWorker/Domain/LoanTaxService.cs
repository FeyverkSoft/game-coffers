﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Types.Account;
using Coffers.Types.Gamer;

namespace Coffers.LoanWorker.Domain
{
    /// <summary>
    /// Обработчик начисляющий пени за просроченный займ
    /// </summary>
    public class LoanTaxService
    {
        private readonly IOperationRepository _oRepository;

        public LoanTaxService(IOperationRepository oRepository)
        {
            _oRepository = oRepository;
        }

        internal async Task ProcessExpireLoan(Loan loan)
        {
            //если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.Tariff.ExpiredLoanTax <= 0)
                return;
            //если он ещё не стух по дате или отменён, то и начислять нечего
            // такого быть не должно...
            if (loan.LoanStatus == LoanStatus.Paid ||
                loan.LoanStatus == LoanStatus.Canceled ||
                loan.ExpiredDate > DateTime.Now
               )
                return;
            var days = (DateTime.Now.Trunc(DateTruncType.Day) - loan.ExpiredDate.Trunc(DateTruncType.Day)).Days;

            if ((DateTime.UtcNow - loan.ExpiredDate).TotalDays > 60)
                return;

            var loanAmount = loan.Account.Balance;

            var penaltyAmount = (loanAmount * (loan.Tariff.ExpiredLoanTax / 100)) * days - loan.PenaltyAmount;

            //если штраф уже был учтён, то скипаем
            if (penaltyAmount == 0)
                return;

            if (penaltyAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(penaltyAmount), "Сумма штрафа к начислению отрицательная. Такого быть недолжно");

            loan.IncrimentPenaltyAmount(penaltyAmount);

            //хитрая логика генерации последовательного GUID.
            //моет конечно и стрельнуть, но пока что при небольших объёмах всё будет пучком
            var timeByte = new List<byte>(BitConverter.GetBytes(DateTime.UtcNow.Trunc(DateTruncType.Day).Ticks))
            {
                (byte)OperationType.Loan
            };
            timeByte.AddRange(loan.Id.ToByteArray());

            await _oRepository.Save(new Operation
            (
                new Guid(timeByte.Take(16).ToArray()),
                loan.Id,
                penaltyAmount,
                OperationType.LoanTax,
                "loan penalty tax",
                loan.Account
            ));
        }

        internal async Task ProcessTaxLoan(Loan loan)
        {
            //если просрочка займа не облагается штрафом, то скипаем займ
            if (loan.Tariff.LoanTax <= 0)
                return;

            // если он не активный, или дата стухани уже наступила, то такой займ не облагается ежедневным процентом.
            if (loan.LoanStatus != LoanStatus.Active && loan.ExpiredDate < DateTime.Now)
                return;

            var days = (DateTime.Now.Trunc(DateTruncType.Day) - loan.ExpiredDate.Trunc(DateTruncType.Day)).Days;

          //  var loanAmount = loan.Account.Balance > loan.Amount ? loan.Amount : loan.Account.Balance;

            var taxAmount = (loan.Amount * (loan.Tariff.LoanTax / 100)) * days - loan.TaxAmount;

            //если процент уже был учтён, то скипаем
            if (taxAmount == 0)
                return;

            if (taxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(taxAmount), "Сумма процента к начислению отрицательная. Такого быть недолжно");
            //throw new NotImplementedException();

            loan.IncrimentTaxAmount(taxAmount);

            //хитрая логика генерации последовательного GUID.
            //моет конечно и стрельнуть, но пока что при небольших объёмах всё будет пучком
            var timeByte = new List<byte>(BitConverter.GetBytes(DateTime.UtcNow.Trunc(DateTruncType.Day).Ticks));
            timeByte.Add((byte)OperationType.Tax);
            timeByte.AddRange(loan.Id.ToByteArray());

            await _oRepository.Save(new Operation
            (
                new Guid(timeByte.Take(16).ToArray()),
                loan.Id,
                taxAmount,
                OperationType.LoanTax,
                "loan tax",
                loan.Account
            ));
        }
    }
}
