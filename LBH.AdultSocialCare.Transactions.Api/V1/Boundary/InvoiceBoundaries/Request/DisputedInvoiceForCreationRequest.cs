using System;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request
{
    public class DisputedInvoiceForCreationRequest
    {
        [Required] public int? ActionRequiredFromId { get; set; }
        [Required] public string ReasonForHolding { get; set; }
    }
}
