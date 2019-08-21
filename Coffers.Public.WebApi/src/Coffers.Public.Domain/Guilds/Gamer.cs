using System;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Guilds
{
    public sealed class Gamer
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// гильдия к которой принадлежит игрок
        /// </summary>
        public Guild Guild { get; private set; }

        /// <summary>
        /// Счёт игрока по умолчанию
        /// </summary>
        public Account DefaultAccount { get; private set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; private set; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; private set; }

        internal Gamer() { }

        /// <summary>
        /// Activated gamer
        /// </summary>
        /// <param name="status"></param>
        public void Activate()
        {
            if (Status != GamerStatus.Active)
            {
                Status = GamerStatus.Active;
                Rank = GamerRank.Beginner;
                UpdateDate = DateTime.UtcNow;
            }
        }

        public Gamer(Guid id, String name, String login, DateTime dateOfBirth, GamerStatus gamerStatus, GamerRank rank)
        {
            Id = id;
            UpdateDate = DateTime.UtcNow;
            CreateDate = DateTime.UtcNow;
            Login = login.Trim();
            Name = name.Trim();
            DateOfBirth = dateOfBirth;
            Status = gamerStatus;
            Rank = rank;
            DefaultAccount = new Account();
        }
    }
}
