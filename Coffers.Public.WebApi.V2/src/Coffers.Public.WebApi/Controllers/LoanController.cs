using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Coffers.Helpers;
using Coffers.Public.Domain.Loans;
using Coffers.Public.Queries.Loans;
using Coffers.Public.WebApi.Authorization;
using Coffers.Public.WebApi.Exceptions;
using Coffers.Public.WebApi.Models.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Core;

namespace Coffers.Public.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(401)]
    public class LoanController : ControllerBase
    {
        /// <summary>
        /// This method add new loan for user
        /// for officer and leader
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="creator"></param>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("admin", "officer", "leader")]
        [HttpPost("/loans")]
        [ProducesResponseType(typeof(LoanView), 200)]
        public async Task<IActionResult> AddNewLoan(
            [FromBody] AddLoanBinding binding,
            [FromServices] LoanCreationService creator,
            [FromServices] ILoanRepository repository,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            try{
                var loan = await creator.CreateLoan(
                    binding.LoanId,
                    binding.UserId,
                    HttpContext.GetGuildId(),
                    binding.Amount,
                    binding.Description,
                    14,
                    cancellationToken);
                await repository.Save(loan, cancellationToken);
            }
            catch (UserNotFoundException e){
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.GamerNotFound, e.Message);
            }
            catch (LoanAlreadyExistsException e){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.LoanAlreadyExists, $"Loan {binding.LoanId} already exists", e.Detail.ToDictionary());
            }

            return Ok(await queryProcessor.Process<LoanViewQuery, LoanView>(new LoanViewQuery(binding.LoanId), cancellationToken));
        }

        /// <summary>
        /// This method cancel Loan
        /// for officer and leader
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "admin")]
        [HttpPost("/loans/{id}/cancel")]
        [ProducesResponseType(typeof(LoanView), 200)]
        public async Task<IActionResult> CancelLoan(
            [FromServices] ILoanRepository repository,
            [FromRoute] Guid id,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            var loan = await repository.Get(id, cancellationToken);

            if (loan == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.LoanNotFound, $"Loan {id} not found");

            try{
                loan.MakeCancel();
                await repository.Save(loan, cancellationToken);
            }
            catch (InvalidOperationException){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect loan state");
            }

            return Ok(await queryProcessor.Process<LoanViewQuery, LoanView>(new LoanViewQuery(id), cancellationToken));
        }

        /// <summary>
        /// This method process Loan
        /// принудительное проведение займа, если что-то пошло не так, и сообщение потерялось в шине
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "veteran", "admin")]
        [HttpPost("/loans/{id}/process")]
        [ProducesResponseType(typeof(LoanView), 200)]
        public async Task<IActionResult> ProcessLoan(
            [FromServices] ILoanRepository repository,
            [FromRoute] Guid id,
            [FromServices] LoanProcessor processor,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            var loan = await repository.Get(id, cancellationToken);

            if (loan == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.LoanNotFound, $"Loan {id} not found");

            try{
                await processor.Process(loan, cancellationToken);
                await repository.Save(loan, cancellationToken);
            }
            catch (InvalidOperationException){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect loan state");
            }

            return Ok(await queryProcessor.Process<LoanViewQuery, LoanView>(new LoanViewQuery(id), cancellationToken));
        }
        
        /// <summary>
        /// This method prolong Loan
        /// for officer and leader
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [PermissionRequired("officer", "leader", "admin")]
        [HttpPost("/loans/{id}/prolong")]
        [ProducesResponseType(typeof(LoanView), 200)]
        public async Task<IActionResult> ProlongLoan(
            [FromServices] ILoanRepository repository,
            [FromRoute] Guid id,
            [FromServices] IQueryProcessor queryProcessor,
            CancellationToken cancellationToken)
        {
            var loan = await repository.Get(id, cancellationToken);

            if (loan == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.LoanNotFound, $"Loan {id} not found");

            try{
                loan.Prolong();
                await repository.Save(loan, cancellationToken);
            }
            catch (InvalidOperationException){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.IncorrectOperation, $"Incorrect loan state");
            }

            return Ok(await queryProcessor.Process<LoanViewQuery, LoanView>(new LoanViewQuery(id), cancellationToken));
        }
    }
}