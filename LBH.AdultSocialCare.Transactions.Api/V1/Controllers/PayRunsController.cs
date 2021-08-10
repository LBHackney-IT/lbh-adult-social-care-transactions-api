using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/pay-runs")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class PayRunsController : ControllerBase
    {
        private readonly ICreatePayRunUseCase _createPayRunUseCase;
        private readonly IGetPayRunSummaryListUseCase _getPayRunSummaryListUseCase;
        private readonly IGetUniqueSuppliersInPayRunUseCase _getUniqueSuppliersInPayRunUseCase;
        private readonly IGetReleasedHoldsCountUseCase _getReleasedHoldsCountUseCase;
        private readonly IGetUniquePackageTypesInPayRunUseCase _getUniquePackageTypesInPayRunUseCase;
        private readonly IGetReleasedHoldsUseCase _getReleasedHoldsUseCase;
        private readonly IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase _getUniqueInvoiceItemPaymentStatusInPayRunUseCase;
        private readonly IGetSinglePayRunDetailsUseCase _getSinglePayRunDetailsUseCase;
        private readonly IChangePayRunStatusUseCase _changePayRunStatusUseCase;
        private readonly IReleaseHeldPaymentsUseCase _releaseHeldPaymentsUseCase;
        private readonly IInvoicesUseCase _invoicesUseCase;
        private readonly IPayRunUseCase _payRunUseCase;
        private readonly IInvoiceStatusUseCase _invoiceStatusUseCase;

        public PayRunsController(ICreatePayRunUseCase createPayRunUseCase, IGetPayRunSummaryListUseCase getPayRunSummaryListUseCase,
            IGetUniqueSuppliersInPayRunUseCase getUniqueSuppliersInPayRunUseCase, IGetReleasedHoldsCountUseCase getReleasedHoldsCountUseCase,
            IGetUniquePackageTypesInPayRunUseCase getUniquePackageTypesInPayRunUseCase, IGetReleasedHoldsUseCase getReleasedHoldsUseCase,
            IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase getUniqueInvoiceItemPaymentStatusInPayRunUseCase,
            IGetSinglePayRunDetailsUseCase getSinglePayRunDetailsUseCase, IChangePayRunStatusUseCase changePayRunStatusUseCase,
            IReleaseHeldPaymentsUseCase releaseHeldPaymentsUseCase, IInvoicesUseCase invoicesUseCase,
            IPayRunUseCase payRunUseCase,
            IInvoiceStatusUseCase invoiceStatusUseCase)
        {
            _createPayRunUseCase = createPayRunUseCase;
            _getPayRunSummaryListUseCase = getPayRunSummaryListUseCase;
            _getUniqueSuppliersInPayRunUseCase = getUniqueSuppliersInPayRunUseCase;
            _getReleasedHoldsCountUseCase = getReleasedHoldsCountUseCase;
            _getUniquePackageTypesInPayRunUseCase = getUniquePackageTypesInPayRunUseCase;
            _getReleasedHoldsUseCase = getReleasedHoldsUseCase;
            _getUniqueInvoiceItemPaymentStatusInPayRunUseCase = getUniqueInvoiceItemPaymentStatusInPayRunUseCase;
            _getSinglePayRunDetailsUseCase = getSinglePayRunDetailsUseCase;
            _changePayRunStatusUseCase = changePayRunStatusUseCase;
            _releaseHeldPaymentsUseCase = releaseHeldPaymentsUseCase;
            _invoicesUseCase = invoicesUseCase;
            _payRunUseCase = payRunUseCase;
            _invoiceStatusUseCase = invoiceStatusUseCase;
        }

        [HttpPost("{payRunType}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> CreateNewPayRun(string payRunType, [FromBody] PayRunForCreationRequest payRunForCreationRequest)
        {
            var result = await _createPayRunUseCase.CreateNewPayRunUseCase(payRunType, payRunForCreationRequest.DateTo).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("date-of-last-pay-run/{payRunType}")]
        [ProducesResponseType(typeof(PayRunDateSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PayRunDateSummaryResponse>> GetDateSummaryOfLastPayRun(string payRunType)
        {
            var result = await _payRunUseCase.GetDateOfLastPayRunUseCase(payRunType).ConfigureAwait(false);
            return Ok(result);
        }

        [ProducesResponseType(typeof(PagedPayRunSummaryResponse), StatusCodes.Status200OK)]
        [HttpGet("summary-list")]
        public async Task<ActionResult<PagedPayRunSummaryResponse>> GetPayRunSummaryList([FromQuery] PayRunSummaryListParameters parameters)
        {
            var res = await _getPayRunSummaryListUseCase.Execute(parameters).ConfigureAwait(false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.PagingMetaData));
            return Ok(res);
        }

        [ProducesResponseType(typeof(PagedSupplierMinimalListResponse), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/unique-suppliers")]
        public async Task<ActionResult<PagedSupplierMinimalListResponse>> GetUniqueSuppliersInPayRun(Guid payRunId, [FromQuery] SupplierListParameters parameters)
        {
            var res = await _getUniqueSuppliersInPayRunUseCase.Execute(payRunId, parameters).ConfigureAwait(false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.PagingMetaData));
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<PackageTypeResponse>), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/unique-package-types")]
        public async Task<ActionResult<IEnumerable<PackageTypeResponse>>> GetUniquePackageTypesInPayRun(Guid payRunId)
        {
            var res = await _getUniquePackageTypesInPayRunUseCase.Execute(payRunId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<ReleasedHoldsByTypeResponse>), StatusCodes.Status200OK)]
        [HttpGet("released-holds-count")]
        public async Task<ActionResult<IEnumerable<ReleasedHoldsByTypeResponse>>> GetReleasedHoldsCountByType([FromQuery] DateTimeOffset? fromDate, [FromQuery] DateTimeOffset? toDate)
        {
            var res = await _getReleasedHoldsCountUseCase.Execute(fromDate, toDate).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceResponse>), StatusCodes.Status200OK)]
        [HttpGet("released-holds")]
        public async Task<ActionResult<IEnumerable<InvoiceResponse>>> GetReleasedHolds([FromQuery] DateTimeOffset? fromDate, [FromQuery] DateTimeOffset? toDate)
        {
            var res = await _getReleasedHoldsUseCase.Execute(fromDate, toDate).ConfigureAwait(false);
            return Ok(res);
        }

        [HttpPost("{payRunId}/invoices/{invoiceId}/hold-payment")]
        [ProducesResponseType(typeof(DisputedInvoiceFlatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DisputedInvoiceFlatResponse>> HoldInvoicePayment(Guid payRunId, Guid invoiceId, [FromBody] DisputedInvoiceForCreationRequest disputedInvoiceForCreationRequest)
        {
            var result = await _invoicesUseCase
                .HoldInvoicePaymentUseCase(payRunId, invoiceId, disputedInvoiceForCreationRequest.ToDomain())
                .ConfigureAwait(false);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/unique-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceStatusResponse>>> GetUniqueInvoiceItemPaymentStatusInPayRun(Guid payRunId)
        {
            var res = await _getUniqueInvoiceItemPaymentStatusInPayRunUseCase.Execute(payRunId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(PayRunDetailsResponse), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/details")]
        public async Task<ActionResult<PayRunDetailsResponse>> GetSinglePayRunDetails(Guid payRunId, [FromQuery] InvoiceListParameters parameters)
        {
            var res = await _getSinglePayRunDetailsUseCase.Execute(payRunId, parameters).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/status/submit-for-approval")]
        public async Task<ActionResult<bool>> SubmitPayRunForApproval(Guid payRunId)
        {
            var res = await _changePayRunStatusUseCase.SubmitPayRunForApproval(payRunId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/status/approve-pay-run")]
        public async Task<ActionResult<bool>> ApprovePayRun(Guid payRunId)
        {
            var res = await _payRunUseCase.ApprovePayRunForPaymentUseCase(payRunId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/status/kick-back-to-draft")]
        public async Task<ActionResult<bool>> KickPayRunBackToDraft(Guid payRunId)
        {
            var res = await _changePayRunStatusUseCase.KickBackPayRunToDraft(payRunId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPut("release-held-invoice")]
        public async Task<ActionResult<bool>> ReleaseSingleHeldInvoice([FromBody] ReleaseHeldInvoiceItemRequest releaseHeldInvoiceItemRequest)
        {
            var res = await _releaseHeldPaymentsUseCase
                .ReleaseHeldInvoiceItemPayment(releaseHeldInvoiceItemRequest.ToDomain()).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPut("{payRunId}/invoices/{invoiceId}/status/reject-invoice")]
        public async Task<ActionResult<bool>> RejectInvoiceInPayRun(Guid payRunId, Guid invoiceId)
        {
            var res = await _payRunUseCase
                .RejectInvoiceInPayRun(payRunId, invoiceId).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPut("release-held-invoice-list")]
        public async Task<ActionResult<bool>> ReleaseHeldInvoiceItemPayment([FromBody] IEnumerable<ReleaseHeldInvoiceItemRequest> releaseHeldInvoiceItemRequests)
        {
            var res = await _releaseHeldPaymentsUseCase
                .ReleaseHeldInvoiceItemPaymentList(releaseHeldInvoiceItemRequests.ToDomain()).ConfigureAwait(false);
            return Ok(res);
        }

        [HttpGet("{payRunId}/summary-insights")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> GetSinglePayRunSummaryInsights(Guid payRunId)
        {
            var result = await _payRunUseCase.GetSinglePayRunInsightsUseCase(payRunId).ConfigureAwait(false);
            return Ok(result);
        }

        // Accept invoice in pay run
        [HttpPut("{payRunId}/invoices/{invoiceId}/accept-invoice")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> ApproveInvoice(Guid payRunId, Guid invoiceId)
        {
            var result = await _invoiceStatusUseCase.AcceptInvoiceUseCase(payRunId, invoiceId).ConfigureAwait(false);
            return Ok(result);
        }

        // Accept list of invoices in pay run
        [HttpPut("{payRunId}/invoices/accept-invoices")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> ApproveInvoices(Guid payRunId, [FromBody] InvoiceIdListRequest invoiceIdList)
        {
            var result = false;
            foreach (var invoiceItem in invoiceIdList.InvoiceIds)
            {
                result = await _invoiceStatusUseCase.AcceptInvoiceUseCase(payRunId, invoiceItem).ConfigureAwait(false);
            }
            return Ok(result);
        }

        // Delete Pay Run
        [HttpDelete("{payRunId}")]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<bool>> DeleteDraftPayRun(Guid payRunId)
        {
            var result = await _payRunUseCase.DeleteDraftPayRunUseCase(payRunId).ConfigureAwait(false);
            return Ok(result);
        }

        // Create disputed invoice chat
        [HttpPost("{payRunId}/create-held-chat")]
        [ProducesResponseType(typeof(DisputedInvoiceChatResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DisputedInvoiceChatResponse>> CreateDisputedInvoiceChat(Guid payRunId,
            [FromBody] DisputedInvoiceChatForCreationRequest disputedInvoiceChatForCreationRequest)
        {
            var result = await _invoicesUseCase
                .CreateDisputedInvoiceChatUseCase(disputedInvoiceChatForCreationRequest.ToDomain(payRunId))
                .ConfigureAwait(false);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<PayRunTypeResponse>), StatusCodes.Status200OK)]
        [HttpGet("pay-run-types")]
        public async Task<ActionResult<IEnumerable<PayRunTypeResponse>>> GetAllPayRunTypes()
        {
            var res = await _payRunUseCase.GetAllPayRunTypesUseCase().ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<PayRunSubTypeResponse>), StatusCodes.Status200OK)]
        [HttpGet("pay-run-sub-types")]
        public async Task<ActionResult<IEnumerable<PayRunSubTypeResponse>>> GetAllPayRunSubTypes()
        {
            var res = await _payRunUseCase.GetAllPayRunSubTypesUseCase().ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<PayRunStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("unique-pay-run-statuses")]
        public async Task<ActionResult<IEnumerable<PayRunStatusResponse>>> GetAllUniquePayRunStatuses()
        {
            var res = await _payRunUseCase.GetAllUniquePayRunStatusesUseCase().ConfigureAwait(false);
            return Ok(res);
        }
    }
}
