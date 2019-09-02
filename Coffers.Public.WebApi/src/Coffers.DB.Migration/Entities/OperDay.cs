using System;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class OperDay
    {
        /// <summary>
        /// Гильдия
        /// </summary>
        public Guid GuildId { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Баланс гильдии
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Сумма налогов собранная на конец отчётного периуда
        /// </summary>
        public Decimal Tax { get; set; }

        /// <summary>
        /// Баланс пользователей
        /// </summary>
        public Decimal UsersBalance { get; set; }

        /// <summary>
        /// Баланс займов
        /// </summary>
        public Decimal LoansBalance { get; set; }

        /// <summary>
        /// Количество юзеров
        /// </summary>
        public Int32 UserCount { get; set; }

        /// <summary>
        /// Сумма штрафов
        /// </summary>
        public Int32 PenaltyAmount { get; set; }

    }
}
