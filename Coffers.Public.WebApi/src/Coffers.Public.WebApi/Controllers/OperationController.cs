using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Coffers.Public.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(401)]
    public class OperationController : ControllerBase
    {
    }
}