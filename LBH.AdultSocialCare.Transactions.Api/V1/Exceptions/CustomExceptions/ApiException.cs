using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ValidationErrorCollection Errors { get; set; }

        public ApiException(string message,
            int statusCode = 500,
            ValidationErrorCollection errors = null) :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
