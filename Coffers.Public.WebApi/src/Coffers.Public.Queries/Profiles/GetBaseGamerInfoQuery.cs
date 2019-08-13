using System;
using Query.Core;

namespace Coffers.Public.Queries.Profiles
{
    public sealed class GetBaseGamerInfoQuery : IQuery<BaseGamerInfoView>
    {
        public Guid UserId { get; set; }
    }
}