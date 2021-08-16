using System;

namespace Infrastructure.Domain.InvoicesDomains
{
    public class DisputedInvoiceForCreationDomain
    {
        public Guid PayRunItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public int ActionRequiredFromId { get; set; }
        public string ReasonForHolding { get; set; }
    }
}
