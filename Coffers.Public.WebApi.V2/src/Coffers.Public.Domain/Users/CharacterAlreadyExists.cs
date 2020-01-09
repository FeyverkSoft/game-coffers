using System;
namespace Coffers.Public.Domain.Users
{
    public sealed class CharacterAlreadyExists : Exception
    {
        public Character Character { get; }
        public CharacterAlreadyExists(Character ch) : base($"Character already exists")
        {
            Character = ch;
        }
    }
}
