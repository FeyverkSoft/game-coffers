using System;

namespace Coffers.Public.Queries.Gamers
{
    /// <summary>
    /// Описание персонажа
    /// </summary>
    public class CharacterView
    {
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

        public CharacterView(String name, String className, Boolean isMain) => (Name, ClassName, IsMain) = (name, className, isMain);
    }
}