using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Queries.Operations;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class OperationsController : ControllerBase
    {
        // private readonly IOerationRepository _operationRepository;
        private readonly IQueryProcessor _queryProcessor;

        public OperationsController(/*IOerationRepository operationRepository, */IQueryProcessor queryProcessor)
        {
            //_operationRepository = operationRepository;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This method Returns all operations by document id.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<OperationView>), 200)]
        public async Task<ActionResult<ICollection<OperationView>>> GetOperations(OperationBinding binding, CancellationToken cancellationToken)
        {
            return Ok(await _queryProcessor.Process<GetOperationsQuery, ICollection<OperationView>>(
                new GetOperationsQuery
                {
                    UserId = binding.UserId,
                    OperationId = binding.OperationId,
                    Type = binding.Type
                }, cancellationToken));
        }
    }
}