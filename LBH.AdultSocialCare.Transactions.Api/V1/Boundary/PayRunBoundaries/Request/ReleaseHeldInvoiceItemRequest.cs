using System;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request
{
    public class ReleaseHeldInvoiceItemRequest
    {
        [Required] public Guid? PayRunId { get; set; }
        [Required] public Guid? InvoiceId { get; set; }
        // [Required] public Guid? InvoiceItemId { get; set; }
    }
}
