using System;

namespace Coffers.Public.Queries.Guilds
{
    /// <summary>
    /// Описание персонажа
    /// </summary>
    public class CharacterView
    {
        /// <summary>
        /// Character id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Имя персонажа
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Класс персонажа
        /// </summary>
        public String ClassName { get; }

        /// <summary>
        /// Признак основного персонажа
        /// </summary>
        public Boolean IsMain { get; }

        public CharacterView(Guid id, String name, String className, Boolean isMain) 
            => (Id, Name, ClassName, IsMain) 
            =  (id, name, className, isMain);
    }
}