using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Exceptions
{
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException() : base("Entity was not found")
        {

        }
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
