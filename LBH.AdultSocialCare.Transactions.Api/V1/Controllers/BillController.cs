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
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using Newtonsoft.Json;

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
        private readonly IPaySupplierBillUseCase _paySupplierBillUseCase;

        public BillController(ICreateSupplierBillUseCase createSupplierBillUseCase,
            IGetBillUseCase getBillUseCase,
            IPaySupplierBillUseCase paySupplierBillUseCase)
        {
            _createSupplierBillUseCase = createSupplierBillUseCase;
            _getBillUseCase = getBillUseCase;
            _paySupplierBillUseCase = paySupplierBillUseCase;
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
        [ProducesResponseType(typeof(PagedBillSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedBillSummaryResponse>> GetBill([FromQuery] BillSummaryListParameters parameters)
        {
            var result = await _getBillUseCase.GetBill(parameters).ConfigureAwait(false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.PagingMetaData));
            return Ok(result);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPost("pay")]
        public async Task<ActionResult<bool>> PaySelectedBill(
            [FromBody] IEnumerable<long> supplierBillIds)
        {
            if (supplierBillIds == null)
            {
                return BadRequest("Object for creation cannot be null.");
            }

            var result = await _paySupplierBillUseCase.PaySupplierBill(supplierBillIds).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
