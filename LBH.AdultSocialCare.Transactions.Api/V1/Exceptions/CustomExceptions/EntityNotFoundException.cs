using System.Net;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions
{
    public class EntityNotFoundException : CustomException
    {
        public int StatusCode { get; set; } = (int) HttpStatusCode.NotFound;
        public EntityNotFoundException() : base("Entity was not found")
        {

        }
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
