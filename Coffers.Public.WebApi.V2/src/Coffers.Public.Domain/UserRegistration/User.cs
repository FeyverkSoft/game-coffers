using System;
using System.Collections.Generic;

using Coffers.Public.Domain.UserRegistration.Events;
using Coffers.Types.Gamer;

using Rabbita.Core;

namespace Coffers.Public.Domain.UserRegistration
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }
        public Guid GuildId { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String? Name { get; }
        /// <summary>
        /// Емайд
        /// </summary>
        public String? Email { get; }

        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; }

        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String? Login { get; }

        public Guid ConcurrencyTokens { get; set; } = Guid.NewGuid();

        public ICollection<IEvent> Events { get; } = new List<IEvent>();

        internal User() { }

        public User(Guid id, Guid guildId, String? name, GamerRank rank, GamerStatus status, DateTime dateOfBirth, String? login, String? email)
            => (Id, GuildId, Name, Rank, Status, DateOfBirth, Login, Email)
                = (id, guildId, name?.Trim(), rank, status, dateOfBirth, login?.Trim(), email);

        internal void ResendConfirmationCode(String confirmationCode)
        {
            if (Status != GamerStatus.New)
                throw new InvalidOperationException($"Invalid state {Status}");

            Events.Add(new ConfirmationCodeCreated(
                confirmationCode: confirmationCode,
                email: Email
            ));
        }

        public void Confirm()
        {
            if (Status != GamerStatus.New)
                throw new InvalidOperationException($"Invalid state {Status}");
            Status = GamerStatus.Active;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
