using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.PayRunUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public PayRunsController(ICreatePayRunUseCase createPayRunUseCase, IGetPayRunSummaryListUseCase getPayRunSummaryListUseCase)
        {
            _createPayRunUseCase = createPayRunUseCase;
            _getPayRunSummaryListUseCase = getPayRunSummaryListUseCase;
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

        [ProducesResponseType(typeof(IEnumerable<PayRunSummaryResponse>), StatusCodes.Status200OK)]
        [HttpGet("summary-list")]
        public async Task<ActionResult<IEnumerable<PayRunSummaryResponse>>> GetPayRunSummaryList()
        {
            return Ok(await _getPayRunSummaryListUseCase.Execute().ConfigureAwait(false));
        }
    }
}
