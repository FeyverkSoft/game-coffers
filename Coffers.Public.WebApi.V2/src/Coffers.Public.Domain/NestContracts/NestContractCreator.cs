using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.NestContracts
{
    public sealed class NestContractCreator
    {
        private readonly INestContractRepository _repository;
        private readonly INestGetter _nestGetter;

        public NestContractCreator(INestContractRepository repository,
            INestGetter nestGetter)
        {
            _repository = repository;
            _nestGetter = nestGetter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="guildId"></param>
        /// <param name="id"></param>
        /// <param name="nestId"></param>
        /// <param name="reward"></param>
        /// <param name="characterName"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="LimitExceededException"></exception>
        /// <returns></returns>
        /// <exception cref="NestNotFoundException"></exception>
        /// <exception cref="ContractAlreadyExistsException"></exception>
        public async Task<NestContract> Create(Guid userId, Guid guildId, Guid id, Guid nestId, String reward, String characterName,
            CancellationToken cancellationToken = default)
        {
            var nest = await _nestGetter.Get(nestId, guildId, cancellationToken);
            if (nest == null)
                throw new NestNotFoundException(nestId);

            var existsContract = await _repository.Get(id, cancellationToken);

            if (existsContract != null)
            {
                if (existsContract.Reward != reward ||
                    existsContract.CharacterName != characterName ||
                    existsContract.UserId != userId ||
                    existsContract.NestId != nestId)
                    throw new ContractAlreadyExistsException(id);
                return existsContract;
            }

            var activeCount = await _repository.GetActiveCount(userId, cancellationToken);
            if (activeCount >= 35)
                throw new LimitExceededException(35);

            var nc = new NestContract(id: id,
                userId: userId,
                nestId: nestId,
                characterName: characterName,
                reward: reward);

            nc.SetTimeOut(168);
            return nc;
        }
    }
}