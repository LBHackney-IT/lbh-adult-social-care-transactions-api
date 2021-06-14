using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/invoices")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class InvoiceController : BaseController
    {
        private readonly IGetUserPendingInvoicesUseCase _getUserPendingInvoicesUseCase;

        public InvoiceController(IGetUserPendingInvoicesUseCase getUserPendingInvoicesUseCase)
        {
            _getUserPendingInvoicesUseCase = getUserPendingInvoicesUseCase;
        }

        [HttpGet("pending/{serviceUserId}")]
        [ProducesResponseType(typeof(IEnumerable<PendingInvoicesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<PendingInvoicesResponse>>> GetUserPendingInvoices(Guid serviceUserId)
        {
            var result = await _getUserPendingInvoicesUseCase.GetUserPendingInvoices(serviceUserId).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
