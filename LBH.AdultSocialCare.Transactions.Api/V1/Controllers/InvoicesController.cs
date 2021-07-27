using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundary.Response;
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
        private readonly IInvoiceStatusUseCase _invoiceStatusUseCase;
        private readonly IInvoicesUseCase _invoicesUseCase;
        private readonly IPayRunUseCase _payRunUseCase;
        private readonly IGetUserPendingInvoicesUseCase _getUserPendingInvoicesUseCase;

        public InvoicesController(IInvoiceStatusUseCase invoiceStatusUseCase, IInvoicesUseCase invoicesUseCase, IPayRunUseCase payRunUseCase,
            IGetUserPendingInvoicesUseCase getUserPendingInvoicesUseCase)
        {
            _invoiceStatusUseCase = invoiceStatusUseCase;
            _invoicesUseCase = invoicesUseCase;
            _payRunUseCase = payRunUseCase;
            _getUserPendingInvoicesUseCase = getUserPendingInvoicesUseCase;
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("invoice-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceStatusResponse>>> GetInvoicePaymentStatusesList()
        {
            var res = await _invoiceStatusUseCase.GetInvoicePaymentStatusesUseCase().ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("invoice-status-list")]
        public async Task<ActionResult<IEnumerable<InvoiceStatusResponse>>> GetAllInvoiceStatusesList()
        {
            var res = await _invoiceStatusUseCase.GetAllInvoiceStatusesUseCase().ConfigureAwait(false);
            return Ok(res);
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
