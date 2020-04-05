using System;
using System.Collections;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Loans
{
    public sealed class LoanView
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; }
        /// <summary>
        /// Сумма выплаченная в пользу займа
        /// </summary>
        public Decimal Balance { get; }

        /// <summary>
        /// Описание на что будет потрачен займ
        /// </summary>
        public String Description { get; }
        /// <summary>
        /// Дата регистрации займа в системе
        /// </summary>
        public DateTime CreateDate { get; }
        /// <summary>
        /// Дата истечения займа
        /// </summary>
        public DateTime ExpiredDate { get; }
        /// <summary>
        /// Статус займа
        /// </summary>
        public LoanStatus LoanStatus { get; }

        public LoanView(Guid id,
            decimal amount,
            decimal balance,
            string description,
            LoanStatus loanStatus,
            DateTime createDate,
            DateTime expiredDate)
        {
            Id = id;
            Amount = amount;
            Balance = balance;
            Description = description;
            CreateDate = createDate;
            ExpiredDate = expiredDate;
            LoanStatus = expiredDate < DateTime.UtcNow &&
                 !((IList)new[] { LoanStatus.Paid, LoanStatus.Canceled, LoanStatus.Expired }).Contains(loanStatus)
                     ? LoanStatus.Expired
                     : loanStatus;
        }
    }
}