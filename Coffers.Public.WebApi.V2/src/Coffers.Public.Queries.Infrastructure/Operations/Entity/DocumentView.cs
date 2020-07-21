using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Infrastructure.Operations.Entity
{
    internal sealed class DocumentView
    {
        internal static readonly String Sql = @"
SELECT 
    p.Id as Id,
    CONCAT(u.Name, ' - ', p.Description) as Description,
    p.UserId as UserId,
    'Penalty' as DocumentType
FROM `Penalty` p
JOIN `User` u ON p.UserId = u.Id
WHERE 1 = 1
AND u.GuildId = @GuildId
AND p.PenaltyStatus IN ('Active')
UNION
SELECT 
    l.Id as Id,
    CONCAT(u.Name,' - ', l.Description) as Description,
    l.UserId as UserId,
    'Loan' as DocumentType
FROM `Loan` l
JOIN `User` u ON l.UserId = u.Id
WHERE 1 = 1
AND u.GuildId = @GuildId
AND l.LoanStatus IN ('Active', 'Expired')
"; 
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Отображаемое описание документа
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType DocumentType { get; }
    }
}