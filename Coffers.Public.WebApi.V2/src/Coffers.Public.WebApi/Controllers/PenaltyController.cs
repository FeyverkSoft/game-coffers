using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class PenaltyController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/penalties")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewPenalty(
            CancellationToken cancellationToken)
        {
            return Ok(new { });
        }
    }
}