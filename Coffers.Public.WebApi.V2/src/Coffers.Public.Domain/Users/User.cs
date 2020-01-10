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
        public DateTime UpdateDate { get; }

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

            Characters.Add(new Character(name, className, isMain));
            ConcurrencyTokens = Guid.NewGuid();
        }

        public void CharacterRemove(Guid characterId)
        {
            var ch = Characters.FirstOrDefault(_ => _.Id == characterId) ?? throw new CharacterNotFound(characterId);

            if (!ch.IsActive)
                return;

            ch.MarkAsDeleted();
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}
