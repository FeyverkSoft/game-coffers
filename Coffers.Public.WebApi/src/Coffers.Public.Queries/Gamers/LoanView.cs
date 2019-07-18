using System;
using System.Collections;
using Coffers.Types.Gamer;
using Newtonsoft.Json;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class LoanView
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Дата когда был взят займ
        /// </summary>
        public DateTime Date { get; set; }

        [JsonIgnore]
        private LoanStatus _loanStatus;
        /// <summary>
        /// Статус займа
        /// </summary>
        public LoanStatus LoanStatus
        {
            get =>
                ExpiredDate < DateTime.UtcNow &&
                !((IList) new[] {LoanStatus.Paid, LoanStatus.Canceled, LoanStatus.Expired}).Contains(_loanStatus)
                    ? LoanStatus.Expired
                    : _loanStatus;
            set => _loanStatus = value;
        }
    }
}
