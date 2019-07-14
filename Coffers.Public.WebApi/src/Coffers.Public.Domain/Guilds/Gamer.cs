using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// гильдия к которой принадлежит игрок
        /// </summary>
        public Guild Guild { get; internal set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; internal set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; internal set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; internal set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; internal set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; internal set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; internal set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; internal set; }
        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; set; }
        internal Gamer() { }

        public Gamer(Guid id, String name, String login, DateTime dateOfBirth, GamerStatus gamerStatus, GamerRank rank)
        {
            Id = id;
            UpdateDate = DateTime.UtcNow;
            CreateDate = DateTime.UtcNow;
            Login = login;
            Name = name;
            DateOfBirth = dateOfBirth;
            Status = gamerStatus;
            Rank = rank;
            DefaultAccount = new Account();
        }
    }
}
