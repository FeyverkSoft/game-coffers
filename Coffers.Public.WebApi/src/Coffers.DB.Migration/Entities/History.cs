using System;

namespace Coffers.DB.Migrations.Entities
{
    /// <summary>
    /// История действий игроков
    /// </summary>
    internal sealed class History
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Игрок
        /// </summary>
        public Gamer Gamer { get; set; }

        /// <summary>
        /// Действие игрока, или над игроком
        /// </summary>
        public String Action { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
