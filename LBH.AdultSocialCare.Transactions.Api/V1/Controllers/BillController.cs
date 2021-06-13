using System;
using System.Collections.Generic;
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
        private readonly ICreateSupplierBillUseCase _createSupplierBillUseCase;
        private readonly IGetBillUseCase _getBillUseCase;

        public BillController(ICreateSupplierBillUseCase createSupplierBillUseCase,
            IGetBillUseCase getBillUseCase)
        {
            _createSupplierBillUseCase = createSupplierBillUseCase;
            _getBillUseCase = getBillUseCase;
        }

        [ProducesResponseType(typeof(BillResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public async Task<ActionResult<BillResponse>> CreateSupplierBill(
            [FromBody] BillCreationRequest billCreationRequest)
        {
            if (billCreationRequest == null)
            {
                return BadRequest("Object for creation cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var billDomain = billCreationRequest.ToDomain();
            var result = await _createSupplierBillUseCase.ExecuteAsync(billDomain).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<BillResponse>>> GetBill([FromQuery] Guid packageId, long supplierId, int billPaymentStatusId, DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null)
        {
            var result = await _getBillUseCase.GetBill(packageId, supplierId, billPaymentStatusId, fromDate,
                toDate).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
