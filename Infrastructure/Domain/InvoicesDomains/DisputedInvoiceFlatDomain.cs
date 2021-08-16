using System;

namespace Infrastructure.Domain.InvoicesDomains
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
