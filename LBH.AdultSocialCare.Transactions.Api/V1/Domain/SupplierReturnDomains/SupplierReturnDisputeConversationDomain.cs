using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains
{
    public class SupplierReturnDisputeConversationDomain
    {
        public Guid DisputeConversationId { get; set; }
        public Guid PackageId { get; set; }
        public bool MessageRead { get; set; }
        public string Message { get; set; }
        public int? MessageFrom { get; set; } //Department or Supplier
        public DateTimeOffset DateCreated { get; set; }
    }
}
