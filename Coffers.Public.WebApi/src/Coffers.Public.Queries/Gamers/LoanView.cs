using System;
using System.Collections;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Gamers
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
        /// Осталось выплатить
        /// </summary>
        public Decimal Balance { get; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; }

        /// <summary>
        /// Дата когда был взят займ
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Описание на что будет потрачен займ
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Статус займа
        /// </summary>
        public LoanStatus LoanStatus { get; }

        public LoanView(Guid id,
            decimal amount,
            decimal balance,
            DateTime createDate,
            string description,
            LoanStatus loanStatus,
            DateTime expiredDate)
        {
            Id = id;
            Amount = amount;
            Balance = balance;
            Date = createDate;
            Description = description;
            ExpiredDate = expiredDate;
            LoanStatus = expiredDate < DateTime.UtcNow &&
                !((IList)new[] { LoanStatus.Paid, LoanStatus.Canceled, LoanStatus.Expired }).Contains(loanStatus)
                    ? LoanStatus.Expired
                    : loanStatus;
        }
    }
}
