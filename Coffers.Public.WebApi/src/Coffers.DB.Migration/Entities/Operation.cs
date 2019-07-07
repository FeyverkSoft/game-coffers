using System;
using System.Collections.Generic;
using System.Text;

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
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата проведения операции
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Счёт с которым проводится операция
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Основание для проведения операции
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// Описание операции
        /// </summary>
        public String Description { get; set; }

    }

    internal enum OperationType
    {
        /// <summary>
        /// Налог
        /// </summary>
        Tax,
        /// <summary>
        /// Штраф
        /// </summary>
        Penalty,
        /// <summary>
        /// Займ
        /// </summary>
        Loan,
        /// <summary>
        /// Обмен
        /// </summary>
        Exchange,
        /// <summary>
        /// Награда
        /// </summary>
        Reward,
        /// <summary>
        /// ЗП
        /// </summary>
        Salary,
        /// <summary>
        /// Иное
        /// </summary>
        Other
    }
}
