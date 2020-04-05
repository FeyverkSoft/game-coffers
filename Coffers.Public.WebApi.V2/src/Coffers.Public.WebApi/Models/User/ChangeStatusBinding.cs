using Coffers.Types.Gamer;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.User
{
    public sealed class ChangeStatusBinding
    {
        /// <summary>
        /// Статус игрока в гильдии
        /// </summary>
        public GamerStatus Status { get; set; }
    }
    public sealed class ChangeStatusBindingValidator : AbstractValidator<ChangeStatusBinding>
    {
        public ChangeStatusBindingValidator()
        {
            RuleFor(r => r.Status)
                .NotNull();
        }
    }

}
