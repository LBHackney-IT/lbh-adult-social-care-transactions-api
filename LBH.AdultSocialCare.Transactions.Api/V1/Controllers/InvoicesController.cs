using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
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
        private readonly IPayRunUseCase _payRunUseCase;
        private readonly IGetUserPendingInvoicesUseCase _getUserPendingInvoicesUseCase;

        public InvoicesController(IGetInvoiceItemPaymentStatusesUseCase getInvoiceItemPaymentStatusesUseCase, IInvoicesUseCase invoicesUseCase, IPayRunUseCase payRunUseCase,
            IGetUserPendingInvoicesUseCase getUserPendingInvoicesUseCase)
        {
            _getInvoiceItemPaymentStatusesUseCase = getInvoiceItemPaymentStatusesUseCase;
            _invoicesUseCase = invoicesUseCase;
            _payRunUseCase = payRunUseCase;
            _getUserPendingInvoicesUseCase = getUserPendingInvoicesUseCase;
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceItemPaymentStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("invoice-item-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceItemPaymentStatusResponse>>> GetInvoiceItemPaymentStatusesList()
        {
            var res = await _getInvoiceItemPaymentStatusesUseCase.Execute().ConfigureAwait(false);
            return Ok(res);
        }

        [HttpPost("{invoiceId}/change-invoice-status")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> ChangeInvoiceStatus(Guid invoiceId, [FromBody] ChangeInvoiceStatusRequest changeInvoiceStatusRequest)
        {
            var result = await _invoicesUseCase.ChangeInvoiceStatusUseCase(invoiceId, changeInvoiceStatusRequest.InvoiceStatusId).ConfigureAwait(false);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<HeldInvoiceResponse>), StatusCodes.Status200OK)]
        [HttpGet("held-invoice-payments")]
        public async Task<ActionResult<IEnumerable<HeldInvoiceResponse>>> GetHeldInvoicePaymentsList()
        {
            var res = await _payRunUseCase.GetHeldInvoicePaymentsUseCase().ConfigureAwait(false);
            return Ok(res);
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

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<InvoiceResponse>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<InvoiceResponse>>> CreateInvoice([FromBody] InvoiceForCreationRequest invoiceForCreationRequest)
        {
            var result = await _invoicesUseCase.CreateInvoiceUseCase(invoiceForCreationRequest.ToDomain()).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
