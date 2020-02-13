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
        /// <summary>
        /// This method add new loan for user
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="creator"></param>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/loans")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddNewLoan(
            [FromBody] AddLoanBinding binding,
            [FromServices] LoanCreationService creator,
            [FromServices] ILoanRepository repository,
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
            catch (UserNotFoundException e)
            {
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, e.Message);
            }
            catch (LoanAlreadyExistsException e)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.LoanAlreadyExists, $"Loan {binding.LoanId} already exists", e.Detail.ToDictionary());
            }

            return Ok(new { });
        }

        /// <summary>
        /// This method cancel Loan
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "admin")]
        [HttpPost("/loans/{id}/cancel")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelPenalty(
            [FromServices] ILoanRepository repository,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var loan = await repository.Get(id, cancellationToken);

            if (loan == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.LoanNotFound, $"Loan {id} not found");

            try
            {
                loan.MakeCancel();
                await repository.Save(loan);
            }
            catch (InvalidOperationException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect loan state");
            }

            return Ok(new { });
        }

        /// <summary>
        /// This method process Loan
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "veteran", "admin")]
        [HttpPost("/loans/{id}/process")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ProcessPenalty(
            [FromServices] ILoanRepository repository,
            [FromRoute] Guid id,
            [FromServices] LoanProcessor processor,
            CancellationToken cancellationToken)
        {
            var loan = await repository.Get(id, cancellationToken);

            if (loan == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.LoanNotFound, $"Loan {id} not found");

            try
            {
                await processor.Process(loan, cancellationToken);
                await repository.Save(loan);
            }
            catch (InvalidOperationException)
            {
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect loan state");
            }

            return Ok(new { });
        }
    }
}