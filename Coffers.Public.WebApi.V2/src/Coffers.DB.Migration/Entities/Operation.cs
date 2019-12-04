using System;
using Coffers.Types.Account;

namespace Coffers.DB.Migrations.Entities
{
    /// <summary>
    /// Операция над счетами
    /// </summary>
    internal sealed class Operation
    {
        /// <summary>
        /// Идентификатор операци
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; set; }

        /// <summary>
        /// Пользователь выполнивший операцию
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Ссылка на родительскую проводку
        /// </summary>
        public Operation ParentOperation { get; set; }
        
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; set; }

    }
}
