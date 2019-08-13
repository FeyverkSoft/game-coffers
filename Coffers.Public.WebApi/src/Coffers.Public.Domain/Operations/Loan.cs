using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Operations
{
    /// <summary>
    /// Сущность хранит займ игрока
    /// </summary>
    public sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Игрок которому принадлежит займ
        /// </summary>
        public Gamer Gamer { get; private set; }

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; private set; }

        /// <summary>
        /// Номер счёта по займу
        /// </summary>
        public Account Account { get; private set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        public LoanStatus LoanStatus { get; private set; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; set; }

        internal void SetStatus(LoanStatus newStatus)
        {
            if (LoanStatus != newStatus)
            {
                UpdateDate = DateTime.UtcNow;
                LoanStatus = newStatus;
                ConcurrencyTokens = Guid.NewGuid();
            }
        }
    }
}
