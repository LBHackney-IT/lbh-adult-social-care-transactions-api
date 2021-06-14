using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/invoices")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class InvoicesController : ControllerBase
    {
        private readonly IGetInvoiceItemPaymentStatusesUseCase _getInvoiceItemPaymentStatusesUseCase;
        private readonly IInvoicesUseCase _invoicesUseCase;

        public InvoicesController(IGetInvoiceItemPaymentStatusesUseCase getInvoiceItemPaymentStatusesUseCase, IInvoicesUseCase invoicesUseCase)
        {
            _getInvoiceItemPaymentStatusesUseCase = getInvoiceItemPaymentStatusesUseCase;
            _invoicesUseCase = invoicesUseCase;
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceItemPaymentStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("invoice-item-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceItemPaymentStatusResponse>>> GetInvoiceItemPaymentStatusesList()
        {
            var res = await _getInvoiceItemPaymentStatusesUseCase.Execute().ConfigureAwait(false);
            return Ok(res);
        }

        [HttpPost("hold-payment")]
        [ProducesResponseType(typeof(DisputedInvoiceFlatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DisputedInvoiceFlatResponse>> CreateNewPayRun([FromBody] DisputedInvoiceForCreationRequest disputedInvoiceForCreationRequest)
        {
            var result = await _invoicesUseCase.HoldInvoicePaymentUseCase(disputedInvoiceForCreationRequest.ToDomain()).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("{invoiceId}/change-invoice-status")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> ChangeInvoiceStatus(Guid invoiceId, [FromBody] ChangeInvoiceStatusRequest changeInvoiceStatusRequest)
        {
            var result = await _invoicesUseCase.ChangeInvoiceStatusUseCase(invoiceId, changeInvoiceStatusRequest.InvoiceStatusId).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
