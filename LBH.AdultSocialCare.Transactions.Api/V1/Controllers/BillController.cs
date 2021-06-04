using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Factories;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.BillUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions.CustomAttributes;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/bills")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class BillController : ControllerBase
    {
        private readonly ICreateBillAsyncUseCase _createBillAsyncUseCase;

        public BillController(ICreateBillAsyncUseCase createBillAsyncUseCase)
        {
            _createBillAsyncUseCase = createBillAsyncUseCase;
        }

        [ProducesResponseType(typeof(BillResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        [HttpPost]
        // [ModelStateValidationFilter]
        public async Task<ActionResult<BillResponse>> CreateBill(
            [FromBody] BillCreationRequest billCreationRequest)
        {
            var billForCreationDomain = billCreationRequest.ToDomain();
            var billResponse =
                await _createBillAsyncUseCase.ExecuteAsync(billForCreationDomain).ConfigureAwait(false);
            return Ok(billResponse);
        }
    }
}
