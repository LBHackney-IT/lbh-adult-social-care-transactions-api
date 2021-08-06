using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains
{
    public class SupplierReturnItemDisputeConversationCreationDomain
    {
        public Guid PackageId { get; set; }
        public Guid SupplierReturnItemId { get; set; }
        public bool MessageRead { get; set; }
        public string Message { get; set; }
        public int? MessageFrom { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
