using System;
using System.Collections.Generic;
using Query.Core;

namespace Coffers.Public.Queries.Operations
{
    public sealed class GetDocumentsQuery : IQuery<ICollection<DocumentView>>
    {
        public Guid GuildId { get; }

        public GetDocumentsQuery(Guid guildId)
            => (GuildId)
                = (guildId);

        public void Deconstruct(out Guid guildId)
            => (guildId)
                = (GuildId);
    }
}