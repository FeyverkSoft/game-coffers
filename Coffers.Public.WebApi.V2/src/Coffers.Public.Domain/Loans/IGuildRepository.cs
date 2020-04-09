﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Loans
{
    public interface IGuildRepository
    {
        Task<Tariff> GetTariff(Guid userId, Guid guildId, CancellationToken cancellationToken);
        Task<Boolean> IsUserExists(Guid userId, Guid guildId, CancellationToken cancellationToken);
    }
}