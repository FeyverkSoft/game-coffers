using Coffers.Types.Gamer;
namespace Coffers.Public.WebApi.Models.Gamers
{
    public class PatchGamerRankBinding
    {
        /// <summary>
        /// New gamer role
        /// </summary>
        public GamerRank Rank { get; set; }
    }
}
