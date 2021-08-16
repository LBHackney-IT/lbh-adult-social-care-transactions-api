using System.Collections.Generic;
using Common.Models;
using Microsoft.AspNetCore.Http;

namespace Common.CustomExceptions
{
    public class InvalidModelStateException : CustomException
    {
        public int StatusCode { get; set; } = StatusCodes.Status422UnprocessableEntity;
        public IEnumerable<ModelStateError> Errors { get; set; }

        public InvalidModelStateException(IEnumerable<ModelStateError> errors, string message = "Please correct the specified errors and try again.") : base(message)
        {
            Errors = errors;
        }
    }
}
