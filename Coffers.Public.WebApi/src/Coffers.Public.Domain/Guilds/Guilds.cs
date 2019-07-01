using System;

namespace Coffers.Public.Domain.Guilds
{
    public class Guild
    {
        public string Id { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateDate { get; private set; }
        
        public DateTime UpdateDate { get; private set; }

        public Guild(string id)
        {
            Id = id;
            IsActive = true;
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

    }
}
