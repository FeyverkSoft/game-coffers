using System;
using Query.Core;

namespace Coffers.Public.Queries.Penalties
{
    public sealed class PenaltyViewQuery : IQuery<PenaltyView>
    {
        /// <summary>
        /// Penalty id
        /// </summary>
        public Guid PenaltyId { get; }

        public PenaltyViewQuery(Guid penaltyId)
            => (PenaltyId)
                = (penaltyId);
    }
}