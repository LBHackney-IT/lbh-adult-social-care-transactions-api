using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class DisputedInvoiceChatResponse
    {
        public Guid DisputedInvoiceChatId { get; set; }
        public Guid DisputedInvoiceId { get; set; }
        public bool MessageRead { get; set; }
        public string Message { get; set; }
        public int? MessageFromId { get; set; }
        public int ActionRequiredFromId { get; set; }
    }
}
