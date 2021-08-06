using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class DisputedInvoiceFlatResponse
    {
        public Guid DisputedInvoiceId { get; set; }
        public Guid PayRunItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public int ActionRequiredFromId { get; set; }
        public string ReasonForHolding { get; set; }
    }
}
