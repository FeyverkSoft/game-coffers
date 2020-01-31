using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    /// <summary>
    /// Сущность хранит займы игрока
    /// </summary>
    internal sealed class Loan
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// игрок
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Тариф по которому проходит займ
        /// </summary>
        public Tariff Tariff { get; }
        public Guid? TariffId { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Необязательное описание, для чего был взят займ
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; }

        /// <summary>
        /// Дата займа
        /// </summary>
        public DateTime BorrowDate { get; }

        /// <summary>
        /// Дата стухания займа
        /// </summary>
        public DateTime ExpiredDate { get; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public Decimal Amount { get; }

        public List<Operation> Operations { get; }

        /// <summary>
        /// Сумма комиссии 
        /// </summary>
        public Decimal TaxAmount { get; }

        /// <summary>
        /// Сумма штрафа 
        /// </summary>
        public Decimal PenaltyAmount { get; }

        public LoanStatus LoanStatus { get; }

        /// <summary>
        /// Токен конкуренции, предназначен для разруливания согласованности данных, при ассинхроных запросаз
        /// </summary>
        public Guid ConcurrencyTokens { get; }
    }

}
