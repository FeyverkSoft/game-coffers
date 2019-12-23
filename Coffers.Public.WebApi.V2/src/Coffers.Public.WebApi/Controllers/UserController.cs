using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Guild;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class UserController : ControllerBase
    {
        private readonly IQueryProcessor _queryProcessor;

        public UserController(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }


        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("gamers")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewGamer(
            [FromBody] GamerCreateBinding binding,
            [FromServices] IUserRepository userRepository,
            [FromServices] UserFactory gamerFactory,
            CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(binding.Id, cancellationToken);

            if (user != null)
                if (user.Login == binding.Login &&
                    user.Name == binding.Name &&
                    user.DateOfBirth == binding.DateOfBirth)
                    return Ok(new { });
                else
                    throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GamerAlreadyExists, "Gamer already exists");

            user = await gamerFactory.Create(binding.Id, HttpContext.GuildId(), binding.Login, binding.Name, binding.DateOfBirth, binding.Rank, binding.Status);

            userRepository.Save(user);

            return Ok(new { });
        }
        /*
                /// <summary>
                /// This method return gamer list
                /// </summary>
                /// <param name="binding"></param>
                /// <param name="cancellationToken"></param>
                /// <returns></returns>
                [Authorize]
                [HttpGet("gamers")]
                [ProducesResponseType(typeof(ICollection<GamersListView>), 200)]
                public async Task<ActionResult<GamersListView>> GetGamers(
                    [FromQuery] GetGamersBinding binding,
                    CancellationToken cancellationToken)
                {
                    return Ok(await _queryProcessor.Process<GetGamersQuery, ICollection<GamersListView>>(
                        new GetGamersQuery
                        {
                            GuildId = HttpContext.GuildId(),
                            Month = binding.DateMonth?.Trunc(DateTruncType.Day),
                            GamerStatuses = binding.GamerStatuses
                        }, cancellationToken
                        ));

                }*/
    }
}