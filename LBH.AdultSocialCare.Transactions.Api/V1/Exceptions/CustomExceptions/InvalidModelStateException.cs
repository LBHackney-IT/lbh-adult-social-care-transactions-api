using LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.Models;
using System.Collections.Generic;
using System.Net;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions
{
    public class InvalidModelStateException : CustomException
    {
        public int StatusCode { get; set; } = (int) HttpStatusCode.UnprocessableEntity;
        public IEnumerable<ModelStateError> Errors { get; set; }

        public InvalidModelStateException(IEnumerable<ModelStateError> errors, string message = "Please correct the specified errors and try again.") : base(message)
        {
            Errors = errors;
        }
    }
}
