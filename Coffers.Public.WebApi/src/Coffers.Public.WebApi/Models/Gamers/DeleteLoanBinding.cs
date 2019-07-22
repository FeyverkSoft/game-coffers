using System;

namespace Coffers.Public.WebApi.Models.Gamers
{
    public sealed class DeleteLoanBinding
    {
        /// <summary>
        /// Идентификатор займа
        /// </summary>
        public Guid Id { get; set; }
    }
}
