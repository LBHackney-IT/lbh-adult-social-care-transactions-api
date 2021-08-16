using System;

namespace Infrastructure.Domain.InvoicesDomains
{
    public class DisputedInvoiceChatForCreationDomain
    {
        public Guid PayRunId { get; set; }
        public Guid InvoiceId { get; set; }
        public string Message { get; set; }
        public int ActionRequiredFromId { get; set; }
    }
}
