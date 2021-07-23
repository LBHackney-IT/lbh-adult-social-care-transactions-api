using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns
{
    public class SupplierReturnItemDisputeConversation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DisputeItemConversationId { get; set; }
        public Guid PackageId { get; set; }
        public Guid SupplierReturnItemId { get; set; }
        public bool MessageRead { get; set; }
        public string Message { get; set; }
        public int? MessageFrom { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
