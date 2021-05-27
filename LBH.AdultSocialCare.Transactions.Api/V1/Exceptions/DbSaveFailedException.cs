using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions
{
    public class DbSaveFailedException : CustomException
    {
        public DbSaveFailedException() : base("Save to db was not successful")
        {
        }

        public DbSaveFailedException(string message) : base(message)
        {
        }
    }
}
