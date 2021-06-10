using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PackageTypeBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;

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

        public PayRunsController(ICreatePayRunUseCase createPayRunUseCase, IGetPayRunSummaryListUseCase getPayRunSummaryListUseCase,
            IGetUniqueSuppliersInPayRunUseCase getUniqueSuppliersInPayRunUseCase, IGetReleasedHoldsCountUseCase getReleasedHoldsCountUseCase,
            IGetUniquePackageTypesInPayRunUseCase getUniquePackageTypesInPayRunUseCase, IGetReleasedHoldsUseCase getReleasedHoldsUseCase,
            IGetUniqueInvoiceItemPaymentStatusInPayRunUseCase getUniqueInvoiceItemPaymentStatusInPayRunUseCase,
            IGetSinglePayRunDetailsUseCase getSinglePayRunDetailsUseCase, IChangePayRunStatusUseCase changePayRunStatusUseCase,
            IReleaseHeldPaymentsUseCase releaseHeldPaymentsUseCase)
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
        }

        [HttpPost("{payRunType}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Guid>> CreateNewPayRun(string payRunType)
        {
            var result = await _createPayRunUseCase.CreateNewPayRunUseCase(payRunType).ConfigureAwait(false);
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

        [ProducesResponseType(typeof(IEnumerable<InvoiceItemMinimalResponse>), StatusCodes.Status200OK)]
        [HttpGet("released-holds")]
        public async Task<ActionResult<IEnumerable<InvoiceItemMinimalResponse>>> GetReleasedHolds([FromQuery] DateTimeOffset? fromDate, [FromQuery] DateTimeOffset? toDate)
        {
            var res = await _getReleasedHoldsUseCase.Execute(fromDate, toDate).ConfigureAwait(false);
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceItemPaymentStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/unique-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceItemPaymentStatusResponse>>> GetUniqueInvoiceItemPaymentStatusInPayRun(Guid payRunId)
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
            var res = await _changePayRunStatusUseCase.ApprovePayRun(payRunId).ConfigureAwait(false);
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
        [HttpPut("release-held-invoice-items")]
        public async Task<ActionResult<bool>> ReleaseHeldInvoiceItemPayment([FromBody] IEnumerable<ReleaseHeldInvoiceItemRequest> releaseHeldInvoiceItemRequests)
        {
            var res = await _releaseHeldPaymentsUseCase.ReleaseHeldInvoiceItemPaymentList(releaseHeldInvoiceItemRequests.ToDomain()).ConfigureAwait(false);
            return Ok(res);
        }
    }
}
