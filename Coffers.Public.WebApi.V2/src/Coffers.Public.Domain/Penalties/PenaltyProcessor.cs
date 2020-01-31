using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Penalties
{
    public sealed class PenaltyProcessor
    {
        private readonly IOperationRepository _operationRepository;
        public PenaltyProcessor(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task Process(
            Penalty penalty,
            CancellationToken cancellationToken)
        {
            if (!penalty.IsActive)
                return;

            var operations = await _operationRepository.Get(penalty.Id, cancellationToken);

            if (operations.Sum(_ => _.Amount) >= penalty.Amount)
                penalty.MakeInActive();

        }
    }
}
