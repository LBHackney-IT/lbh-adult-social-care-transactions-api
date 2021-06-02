using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Controllers
{
    [Route("api/v1/healthcheck")]
    [ApiController]
    [Produces("application/json")]
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        [Route("ping")]
        [ProducesResponseType(typeof(Dictionary<string, bool>), 200)]
        public IActionResult HealthCheck()
        {
            var result = new Dictionary<string, bool> { { "success", true } };

            return Ok(result);
        }

        [HttpGet]
        [Route("error")]
        public void ThrowError()
        {
            ThrowOpsErrorUsecase.Execute();
        }

        [HttpGet("test-exception")]
        public IEnumerable<string> TestExceptions()
        {
            throw new ApiException("New API exception", 500, null);
            // throw new EntityNotFoundException("Entity with id 1 not found");
            // throw new DbSaveFailedException();
            // throw new Exception();
            /*throw new InvalidModelStateException(new List<ModelStateError>()
            {
                new ModelStateError("123", "123 error"),
                new ModelStateError("1234", "1234 error"),
            }, "Invalid model");*/
        }

    }
}
