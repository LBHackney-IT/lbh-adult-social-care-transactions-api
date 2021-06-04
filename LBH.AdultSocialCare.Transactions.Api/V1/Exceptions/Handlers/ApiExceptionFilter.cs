using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions;
using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Handlers
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
#if DEBUG
            Debugger.Break();
#endif
            int statusCode;

            ApiError apiError = null;
            switch (context.Exception)
            {
                case ApiException apiException:
                    // handle explicit 'known' API errors
                    context.Exception = null;
                    apiError = new ApiError(apiException.Message) { Errors = apiException.Errors };
                    statusCode = apiException.StatusCode;
                    break;

                case InvalidModelStateException invalidModelStateException:
                    apiError = new ApiError(invalidModelStateException.Errors, invalidModelStateException.Message);
                    statusCode = invalidModelStateException.StatusCode;
                    break;

                case DbSaveFailedException dbSaveFailedException:
                    apiError = new ApiError(dbSaveFailedException.Message);
                    statusCode = dbSaveFailedException.StatusCode;
                    break;

                case EntityNotFoundException entityNotFoundException:
                    apiError = new ApiError(entityNotFoundException.Message);
                    statusCode = entityNotFoundException.StatusCode;
                    break;

                case UnauthorizedAccessException _:
                    apiError = new ApiError("Unauthorized Access");
                    statusCode = 401;
                    break;

                default:
                    {
                        // Unhandled errors
#if !DEBUG
                        var msg = "An unhandled error occurred.";
                        string stack = null;
#else
                        var msg = context.Exception.GetBaseException().Message;
                        string stack = context.Exception.StackTrace;
#endif

                        apiError = new ApiError(msg) { Detail = stack };

                        statusCode = 500;

                        // handle logging here
                        break;
                    }
            }

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.HttpContext.Response.StatusCode = statusCode;

            // always return a JSON result
            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
