using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;

namespace Coffers.DB.Migrations.Entities
{
    internal sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// гильдия к которой принадлежит игрок
        /// </summary>
        public Guild Guild { get; }
        public Guid GuildId { get; }

        /// <summary>
        /// Список чаров игрока
        /// </summary>
        public List<Character> Characters { get; }

        /// <summary>
        /// Список займов игрока
        /// </summary>
        public List<Loan> Loans { get; }

        /// <summary>
        /// Список налогов юзера
        /// </summary>
        public List<Tax> Taxs { get; }

        /// <summary>
        /// Список штрафов игрока
        /// </summary>
        public List<Penalty> Penalties { get; }

        /// <summary>
        /// Список операций игрока
        /// </summary>

        public List<Operation> Operations { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; }

        /// <summary>
        /// Дата когда игрок удалился из гильдии
        /// </summary>
        public DateTime? DeletedDate { get; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get; }

        /// <summary>
        /// Емайл пользователя
        /// </summary>
        public String? Email { get; }

        /// <summary>
        /// Права доступа игрока, JSON строка
        /// </summary>
        public String Roles { get; }
        public Guid ConcurrencyTokens { get; }

        protected User() { }
        public User(Guid id, string login, GamerRank rank, Guid guildId, string roles, string name, GamerStatus status, DateTime createDate, DateTime updateDate, DateTime dateOfBirth)
            => (Id, Login, Rank, GuildId, Roles, Name, Status, CreateDate, UpdateDate, DateOfBirth)
            = (id, login, rank, guildId, roles, name, status, createDate, updateDate, dateOfBirth);
    }
}
