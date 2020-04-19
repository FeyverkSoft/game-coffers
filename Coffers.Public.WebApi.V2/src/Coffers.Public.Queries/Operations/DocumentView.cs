using System;
using Coffers.Types.Account;

namespace Coffers.Public.Queries.Operations
{
    public sealed class DocumentView
    {
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

        public DocumentView(Guid id, Guid userId, String description, DocumentType documentType)
            => (Id, UserId, Description, DocumentType)
                = (id, userId, description, documentType);
    }
}