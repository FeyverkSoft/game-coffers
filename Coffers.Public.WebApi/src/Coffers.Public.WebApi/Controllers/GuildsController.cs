using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Public.Domain.Guilds;
using Coffers.Public.Queries.Guilds;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GuildsController : ControllerBase
    {
        private readonly IGuildRepository _guildRepository;
        private readonly IQueryProcessor _queryProcessor;

        public GuildsController(IGuildRepository guildRepository, IQueryProcessor queryProcessor)
        {
            _guildRepository = guildRepository;
            _queryProcessor = queryProcessor;
        }

        /// <summary>
        /// This methdod register new Guild in guild coffer service
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create(ClientCreateBinding binding, CancellationToken cancellationToken)
        {
            var existClient = await _guildRepository.Get(binding.Id, cancellationToken);

            if (existClient != null)
                return CreatedAtRoute("GetGuild", new { id = binding.Id }, null);

            var ipAddress = HttpContext.Request.Headers["X-Original-For"].ToString() ??
                            HttpContext.Request.Headers["X-Forwarded-For"].ToString() ??
                            HttpContext.Request.Headers["X-Real-IP"].ToString();


            return CreatedAtRoute("GetGuild", new { id = binding.Id }, null);
        }

        /// <summary>
        /// This method get client model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetGuild")]
        [ProducesResponseType(typeof(GuildView), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var client = await _queryProcessor.Process(new GuildQuery { Id = id }, cancellationToken);

            if (client == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            return Ok(client);
        }


        /// <summary>
        /// This method changes the guild settings
        /// </summary>
        /// <param name="id">Guild identifier</param>
        /// <param name="binding">Guild change model</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ApiException"></exception>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        public async Task<IActionResult> Change([FromRoute] string id, [FromBody] ClientChangeBinding binding, CancellationToken cancellationToken)
        {
            var guild = await _guildRepository.Get(id, cancellationToken);

            if (guild == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GuildNotFound, "Guild not found");

            await _guildRepository.Save(guild);

            return NoContent();
        }
    }
}