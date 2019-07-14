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
    }
}