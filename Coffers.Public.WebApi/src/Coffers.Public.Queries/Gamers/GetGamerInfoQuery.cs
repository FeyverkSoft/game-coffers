using System;
using Query.Core;

namespace Coffers.Public.Queries.Gamers
{
    public sealed class GetGamerInfoQuery : IQuery<GamerInfoView>
    {
        public Guid UserId { get; set; }
    }
}