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
        public string Name { get; }

        /// <summary>
        /// Класс персонажа
        /// </summary>
        public string ClassName { get; }

        public CharacterView(string name, string className)
        {
            Name = name;
            ClassName = className;
        }
    }
}