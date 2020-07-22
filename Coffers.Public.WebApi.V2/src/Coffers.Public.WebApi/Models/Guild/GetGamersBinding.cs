using System;
using System.Collections.Generic;
using Coffers.Types.Gamer;
using FluentValidation;

namespace Coffers.Public.WebApi.Models.Guild
{
    public sealed class GetGamersBinding
    {
        /// <summary>
        /// Date to
        /// </summary>
        public DateTime? DateMonth { get; set; }

        /// <summary>
        /// Gamer statuses list
        /// </summary>
        public List<GamerStatus>? GamerStatuses { get; set; }
    }

    public class GetGamersBindingValidator : AbstractValidator<GetGamersBinding>
    {
        public GetGamersBindingValidator()
        {
            When(x => x.DateMonth != null, () =>
                {
                    RuleFor(y => y.DateMonth)
                        .LessThan(DateTime.Now);
                });
        }
    }
}