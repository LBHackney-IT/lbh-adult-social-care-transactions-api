using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions.CustomAttributes
{
    public class ModelStateValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments.SingleOrDefault(x =>
                x.Value.ToString().Contains("CreationRequest") || x.Value.ToString().Contains("UpdateRequest")).Value;

            if (param == null)
            {
                throw new ApiException($"Object is null. Controller: {controller}, action: {action}", StatusCodes.Status400BadRequest);
            }

            if (!context.ModelState.IsValid)
            {
                throw new InvalidModelStateException(context.ModelState.AllModelStateErrors(),
                    "There are some validation errors. Please correct and try again");
            }
        }
    }
}
