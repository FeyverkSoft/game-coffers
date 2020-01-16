using System;
using System.Collections;
using Coffers.Types.Gamer;

namespace Coffers.Public.Queries.Users
{
    public sealed class LoanView
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Дата когда был взят займ
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; }

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
            string description,
            LoanStatus loanStatus,
            DateTime createDate,
            DateTime expiredDate)
        {
            Id = id;
            Amount = amount;
            Description = description;
            CreateDate = createDate;
            LoanStatus = expiredDate < DateTime.UtcNow &&
                !((IList)new[] { LoanStatus.Paid, LoanStatus.Canceled, LoanStatus.Expired }).Contains(loanStatus)
                    ? LoanStatus.Expired
                    : loanStatus;
        }
    }
}