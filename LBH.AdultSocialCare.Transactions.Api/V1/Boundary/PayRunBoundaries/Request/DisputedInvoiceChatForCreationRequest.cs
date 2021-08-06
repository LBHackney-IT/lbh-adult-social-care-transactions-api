using System;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Request
{
    public class DisputedInvoiceChatForCreationRequest
    {
        [Required] public Guid? PayRunItemId { get; set; }
        [Required] public string Message { get; set; }
        [Required] public int? ActionRequiredFromId { get; set; }
    }
}
