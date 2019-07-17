
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
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// Date from
        /// </summary>
        public DateTime? DateTo { get; set; }
        /// <summary>
        /// Gamer statuses list
        /// </summary>
        public List<GamerStatus> GamerStatuses { get; set; }
    }

    public class GetGamersBindingValidator : AbstractValidator<GetGamersBinding>
    {
        public GetGamersBindingValidator()
        {
            When(x => x.DateFrom != null && x.DateTo != null, () =>
                {
                    RuleFor(y => y.DateFrom)
                        .LessThan(y => y.DateTo);
                });
        }
    }
}