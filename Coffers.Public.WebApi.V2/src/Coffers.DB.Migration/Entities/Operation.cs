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
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор гильдии
        /// </summary>
        public Guid GuildId { get; }
        /// <summary>
        /// Пользователь выполнивший операцию
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Ссылка на родительскую проводку
        /// </summary>
        public Guid? ParentOperationId { get; }
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid? DocumentId { get; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; }

    }
}
