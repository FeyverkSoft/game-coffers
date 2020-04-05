using System.Reflection;
using System.Threading;
using Coffers.Public.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers.Admin
{
    [Route("[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        /// <summary>
        /// Возвращает текущую версию сервиса
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(VersionView), 200)]
        public IActionResult Get(CancellationToken cancellationToken)
        {
            return Ok(new VersionView
            {
                Version = typeof(VersionController).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version
            });
        }
    }
}