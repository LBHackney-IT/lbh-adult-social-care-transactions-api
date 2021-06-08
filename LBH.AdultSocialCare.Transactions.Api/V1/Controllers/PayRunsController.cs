using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public PayRunsController(ICreatePayRunUseCase createPayRunUseCase, IGetPayRunSummaryListUseCase getPayRunSummaryListUseCase, IGetUniqueSuppliersInPayRunUseCase getUniqueSuppliersInPayRunUseCase)
        {
            _createPayRunUseCase = createPayRunUseCase;
            _getPayRunSummaryListUseCase = getPayRunSummaryListUseCase;
            _getUniqueSuppliersInPayRunUseCase = getUniqueSuppliersInPayRunUseCase;
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

        [ProducesResponseType(typeof(IEnumerable<PagedPayRunSummaryResponse>), StatusCodes.Status200OK)]
        [HttpGet("summary-list")]
        public async Task<ActionResult<IEnumerable<PagedPayRunSummaryResponse>>> GetPayRunSummaryList([FromQuery] PayRunSummaryListParameters parameters)
        {
            var res = await _getPayRunSummaryListUseCase.Execute(parameters).ConfigureAwait(false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.PagingMetaData));
            return Ok(res);
        }

        [ProducesResponseType(typeof(IEnumerable<PagedSupplierMinimalListResponse>), StatusCodes.Status200OK)]
        [HttpGet("{payRunId}/unique-suppliers")]
        public async Task<ActionResult<IEnumerable<PagedSupplierMinimalListResponse>>> GetUniqueSuppliersInPayRun(Guid payRunId, [FromQuery] SupplierListParameters parameters)
        {
            var res = await _getUniqueSuppliersInPayRunUseCase.Execute(payRunId, parameters).ConfigureAwait(false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(res.PagingMetaData));
            return Ok(res);
        }
    }
}
