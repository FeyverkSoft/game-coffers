using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(VersionView), 200)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(new VersionView
            {
                Version = typeof(RuntimeEnvironment).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version
            });
        }
    }
}