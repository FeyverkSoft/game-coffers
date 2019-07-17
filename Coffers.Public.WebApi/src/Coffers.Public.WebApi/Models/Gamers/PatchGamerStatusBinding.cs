using Coffers.Types.Gamer;
namespace Coffers.Public.WebApi.Models.Gamers
{
    public class PatchGamerStatusBinding
    {
        /// <summary>
        /// New gamer status
        /// </summary>
        public GamerStatus Status { get; set; }
    }
}
