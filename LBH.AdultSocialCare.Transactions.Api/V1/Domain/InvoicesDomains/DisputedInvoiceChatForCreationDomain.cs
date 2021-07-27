using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class DisputedInvoiceChatForCreationDomain
    {
        public Guid PayRunId { get; set; }
        public Guid PayRunItemId { get; set; }
        public string Message { get; set; }
        public int ActionRequiredFromId { get; set; }
    }
}
