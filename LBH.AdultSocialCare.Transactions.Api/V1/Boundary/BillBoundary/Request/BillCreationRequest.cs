using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Request
{
    public class BillCreationRequest
    {
        [Required] public int SupplierId { get; set; }
        [Required] public string Ref { get; set; }
        [Required] public Guid PackageId { get; set; }
        [Required] public DateTimeOffset DateEntered { get; set; }
        [Required] public DateTimeOffset DateDue { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public decimal AmountPaid { get; set; }
        public int? BillStatusId { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
