using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Coffers.Public.Domain.UserRegistration;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public sealed class RegistrateController : ControllerBase
    {
        /// <summary>
        /// Регистрация пользователя в сервисе по емайлу
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("byemail")]
        [ProducesResponseType(typeof(TokenView), 200)]
        public async Task<IActionResult> RegByEmail(
            [FromBody] AuthEmailBinding binding,
            [FromServices] UserRegistrarService registrar,
            [FromServices] IUserRegistrationRepository repository,
            CancellationToken cancellationToken)
        {
            try
            {
                var user = await registrar.CreateByEmail(
                   id: binding.Id,
                   guildId: binding.GuildId,
                   password: binding.Password.Trim(),
                   email: binding.Email,
                   name: binding.Name,
                   cancellationToken: cancellationToken
                   );

                await repository.Save(user, cancellationToken);
                registrar.ResendConfirmationCode(user);

                return Ok();
            }
            catch (UserAlreadyExistsException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.GamerAlreadyExists, "Gamer already exists");
            }
        }
    }
}
