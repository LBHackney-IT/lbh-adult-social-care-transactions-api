using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions.CustomExceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }
    }
}
