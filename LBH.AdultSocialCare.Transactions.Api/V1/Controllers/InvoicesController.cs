using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.InvoiceUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public InvoicesController(IGetInvoiceItemPaymentStatusesUseCase getInvoiceItemPaymentStatusesUseCase)
        {
            _getInvoiceItemPaymentStatusesUseCase = getInvoiceItemPaymentStatusesUseCase;
        }

        [ProducesResponseType(typeof(IEnumerable<InvoiceItemPaymentStatusResponse>), StatusCodes.Status200OK)]
        [HttpGet("invoice-item-payment-statuses")]
        public async Task<ActionResult<IEnumerable<InvoiceItemPaymentStatusResponse>>> GetInvoiceItemPaymentStatusesList()
        {
            var res = await _getInvoiceItemPaymentStatusesUseCase.Execute().ConfigureAwait(false);
            return Ok(res);
        }
    }
}
