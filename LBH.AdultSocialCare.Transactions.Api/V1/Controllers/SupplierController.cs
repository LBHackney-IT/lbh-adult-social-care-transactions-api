using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/suppliers")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class SupplierController : BaseController
    {
        private readonly IGetSuppliersUseCase _getSuppliersUseCase;
        private readonly IGetSupplierTaxRatesUseCase _getSupplierTaxRatesUseCase;

        public SupplierController(IGetSuppliersUseCase getSuppliersUseCase, IGetSupplierTaxRatesUseCase getSupplierTaxRatesUseCase)
        {
            _getSuppliersUseCase = getSuppliersUseCase;
            _getSupplierTaxRatesUseCase = getSupplierTaxRatesUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedSupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedSupplierResponse>> GetSupplier([FromQuery] SupplierListParameters parameters)
        {
            var result = await _getSuppliersUseCase.GetSupplierUseCase(parameters).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{supplierId}/tax-rates")]
        [ProducesResponseType(typeof(IEnumerable<SupplierTaxRateResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<SupplierTaxRateResponse>>> GetSupplierTaxRates(long supplierId)
        {
            var result = await _getSupplierTaxRatesUseCase.GetSupplierTaxRates(supplierId).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
