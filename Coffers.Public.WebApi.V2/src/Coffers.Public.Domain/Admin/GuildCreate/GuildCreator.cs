using System;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Types.Guilds;

namespace Coffers.Public.Domain.Admin.GuildCreate
{
    public sealed class GuildCreator
    {
        private readonly IGuildRepository _repository;

        public GuildCreator(IGuildRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guild> Create(Guid id, String name, GuildStatus status, RecruitmentStatus recruitmentStatus, CancellationToken cancellationToken)
        {
            var guild = await _repository.Get(id, cancellationToken);
            if (guild != null)
            {
                if (guild.Name == name)
                    return guild;
                throw new GuildAlreadyExistsException($"Гильдия {id} уже существует");
            }

            return new Guild(id: id, name: name, status: status, recruitmentStatus: recruitmentStatus);
        }
    }
}