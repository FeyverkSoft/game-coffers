using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coffers.Public.Domain.Operations
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Возвращает результат проверки наличия оили отсотвия займа с указанным идентификатором у указанного пользователя
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Boolean> IsLoanExists(Guid documentId, Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// возаращает результат процерки наличия штрафа с указанным идентификатором у указанного пользователя
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Boolean> IsPenaltyExists(Guid documentId, Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает результат проверки наличия налога у указанного пользователя с указанным идентификаторм
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Boolean> IsTaxExists(Guid documentId, Guid userId, CancellationToken cancellationToken);
    }
}
