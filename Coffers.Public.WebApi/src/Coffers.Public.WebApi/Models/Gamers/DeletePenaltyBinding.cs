using System;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public sealed class DeletePenaltyBinding
    {
        /// <summary>
        /// Идентификатор штрафа
        /// </summary>
        public Guid Id { get; set; }
    }
}
