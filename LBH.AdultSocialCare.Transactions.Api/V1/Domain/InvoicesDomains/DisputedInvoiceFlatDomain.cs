using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class DisputedInvoiceFlatDomain
    {
        public Guid DisputedInvoiceId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public int ActionRequiredFromId { get; set; }
        public string ReasonForHolding { get; set; }
    }
}
