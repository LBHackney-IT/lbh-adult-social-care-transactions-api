using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.SupplierUseCases.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/supplier")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class SupplierController : BaseController
    {
        private readonly IGetSuppliersUseCase _getSuppliersUseCase;

        public SupplierController(IGetSuppliersUseCase getSuppliersUseCase)
        {
            _getSuppliersUseCase = getSuppliersUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SupplierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<SupplierResponse>>> GetSupplier([FromQuery] string query)
        {
            var result = await _getSuppliersUseCase.GetSupplierUseCase(query).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
