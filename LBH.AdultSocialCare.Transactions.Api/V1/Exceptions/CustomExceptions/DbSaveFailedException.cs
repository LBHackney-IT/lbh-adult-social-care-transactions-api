using System.Net;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions
{
    public class DbSaveFailedException : CustomException
    {
        public int StatusCode { get; set; } = (int) HttpStatusCode.InternalServerError;
        public DbSaveFailedException() : base("Save to db was not successful")
        {
        }

        public DbSaveFailedException(string message) : base(message)
        {
        }
    }
}
