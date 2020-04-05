using Coffers.Types.Gamer;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.User
{
    public sealed class ChangeRankBinding
    {
        /// <summary>
        /// Звание игрока
        /// </summary>
        public GamerRank Rank { get; set; }
    }
    public sealed class ChangeRankBindingValidator : AbstractValidator<ChangeRankBinding>
    {
        public ChangeRankBindingValidator()
        {
            RuleFor(r => r.Rank)
                .NotNull();
        }
    }
}
