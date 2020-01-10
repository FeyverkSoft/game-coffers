using System;
namespace Coffers.Public.Domain.Users
{
    public sealed class CharacterNotFound : Exception
    {
        public CharacterNotFound(Guid id) : base($"Character #{id} does not exist") { }
    }
}
