using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Operations;
using Coffers.Public.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class OperationController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader", "veteran")]
        [HttpPost("/operations")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewOperation(
            [FromServices] IOperationsRepository operationsRepository,
            [FromBody] AddOperationBinding binding,
            CancellationToken cancellationToken)
        {
            return Ok(new { });
        }
    }
}