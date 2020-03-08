using System;
using System.Collections.Generic;
using System.Linq;
using Coffers.Types.Gamer;

namespace Coffers.Public.Domain.Users
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public Guid Id { get; }
        public Guid GuildId { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Имя игрока
        /// </summary>
        public String Name { get; }

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
        public DateTime DateOfBirth { get; }

        public ICollection<Character> Characters { get; }

        public Guid ConcurrencyTokens { get; private set; }
        internal User() { }

        public void AddCharacter(String name, String className, Boolean isMain)
        {
            if (Characters
                .Any(_ => _.Name == name
                          && _.ClassName == className
                          && _.IsActive))
                return;

            if (Characters.Any(_ => _.Name == name
                                    && _.IsActive
                                    && _.ClassName != className))
                throw new CharacterAlreadyExists(Characters.First(_ => _.Name == name

                                                                       && _.IsActive));
            if (isMain)
                foreach (var character in Characters)
                {
                    character.UnmarkAsMain();
                }

            Characters.Add(new Character(name, className, isMain));
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        public void SetMainCharacter(Guid characterId)
        {
            var ch = Characters.FirstOrDefault(_ => _.Id == characterId) ?? throw new CharacterNotFound(characterId);
            if (ch.IsMain)
                return;
            foreach (var character in Characters)
            {
                character.UnmarkAsMain();
            }
            ch.MarkAsMain();
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        public void CharacterRemove(Guid characterId)
        {
            var ch = Characters.FirstOrDefault(_ => _.Id == characterId) ?? throw new CharacterNotFound(characterId);

            if (!ch.IsActive)
                return;

            ch.MarkAsDeleted();
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        public void ChangeRank(GamerRank rank)
        {
            if (Rank == rank)
                return;
            Rank = rank;
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }

        public void ChangeStatus(GamerStatus status)
        {
            if (Status == status)
                return;
            Status = status;
            ConcurrencyTokens = Guid.NewGuid();
            UpdateDate = DateTime.UtcNow;
        }
    }
}
