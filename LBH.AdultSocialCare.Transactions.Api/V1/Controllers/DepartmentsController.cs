using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.DepartmentBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase.DepartmentUseCases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/departments")]
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiVersion("1.0")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IGetPaymentDepartmentsUseCase _getPaymentDepartmentsUseCase;

        public DepartmentsController(IGetPaymentDepartmentsUseCase getPaymentDepartmentsUseCase)
        {
            _getPaymentDepartmentsUseCase = getPaymentDepartmentsUseCase;
        }

        [ProducesResponseType(typeof(IEnumerable<DepartmentResponse>), StatusCodes.Status200OK)]
        [HttpGet("payment-departments")]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetPaymentDepartmentList()
        {
            var res = await _getPaymentDepartmentsUseCase.GetPaymentDepartments().ConfigureAwait(false);
            return Ok(res);
        }
    }
}
