using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Loans;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class LoanController : ControllerBase
    {
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/loans")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewLoan(
            [FromBody] AddLoanBinding binding,
            LoanCreationService creator,
            ILoanRepository repository,
            CancellationToken cancellationToken)
        {
            try
            {
                var loan = await creator.CreateLoan(
                    binding.LoanId,
                    binding.UserId,
                    HttpContext.GetGuildId(),
                    binding.Amount,
                    binding.Description,
                    14,
                    cancellationToken);
                await repository.Save(loan);
            }
            catch (LoanAlreadyExistsException e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.LoanAlreadyExists, $"Loan {binding.LoanId} already exists", e.Detail.ToDictionary());
            }

            return Ok(new { });
        }
    }
}