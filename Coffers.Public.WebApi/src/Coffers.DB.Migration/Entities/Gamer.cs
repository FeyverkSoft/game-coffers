using System;
using System.Collections.Generic;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// гильдия к которой принадлежит игрок
        /// </summary>
        public Guild Guild { get; set; }

        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; set; }

        /// <summary>
        /// Список займов игрока
        /// </summary>
        public List<Loan> Loans { get; set; }

        /// <summary>
        /// История действий игрока
        /// </summary>
        public List<History> Histories { get; set; }


        /// <summary>
        /// Список штрафов игрока
        /// </summary>
        public List<Penalty> Penalties { get; set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Дата когда игрок удалился из гильдии
        /// </summary>
        public DateTime DeletedDate { get; set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public string Password { get; set; }
    }

    internal enum GamerStatus
    {
        Afk,
        Banned,
        Active,
        Left,
        New
    }

    internal enum GamerRank
    {
        Leader,
        Officer,
        Veteran,
        Soldier,
        Beginner
    }
}
