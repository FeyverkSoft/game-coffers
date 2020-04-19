using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Queries.Operations;
using Query.Core;
using Dapper;

namespace Coffers.Public.Queries.Infrastructure.Operations
{
    public class OperationsQueryHandler :
        IQueryHandler<GetOperationsQuery, ICollection<OperationListView>>,
        IQueryHandler<GetDocumentsQuery, ICollection<DocumentView>>
    {
        private readonly IDbConnection _db;

        public OperationsQueryHandler(IDbConnection db)
        {
            _db = db;
        }

        /// <summary>
        /// пока что без пагинации, если будет реально много. то добавлю пагинацию
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ICollection<OperationListView>> Handle(GetOperationsQuery query, CancellationToken cancellationToken)
        {
            var dateMonth = (query.DateMonth ?? DateTime.UtcNow).Trunc(DateTruncType.Month);

            var operations = await _db.QueryAsync<Entity.OperationListItem>(Entity.OperationListItem.Sql, new
            {
                GuildId = query.GuildId,
                DateMonth = dateMonth
            });

            return operations.Select(_ => new OperationListView(
                _.Id,
                _.Amount,
                _.CreateDate,
                _.Description,
                _.Type,
                Guid.Empty == _.DocumentId ? null : _.DocumentId,
                Guid.Empty == _.DocumentId ? null : _.DocumentAmount,
                _.DocumentDescription,
                _.UserId,
                _.UserName)).ToImmutableList();
        }

        public async Task<ICollection<DocumentView>> Handle(GetDocumentsQuery query, CancellationToken cancellationToken)
        {
            var documents = await _db.QueryAsync<Entity.DocumentView>(Entity.DocumentView.Sql, new
            {
                GuildId = query.GuildId,
            });
            return documents.Select(_ => new DocumentView(
                id: _.Id,
                userId: _.UserId,
                description: _.Description,
                documentType: _.DocumentType
            )).ToImmutableList();
        }
    }
}