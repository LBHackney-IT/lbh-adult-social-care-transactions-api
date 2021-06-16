using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request
{
    public class InvoiceForCreationRequest
    {
        [Required] public long? SupplierId { get; set; }
        [Required] public int? PackageTypeId { get; set; }
        [Required] public Guid? ServiceUserId { get; set; }
        [Required] public Guid? CreatorId { get; set; }
        public IEnumerable<InvoiceItemForCreationRequest> InvoiceItems { get; set; }
    }
}
