using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request
{
    public class ChangeInvoiceItemPaymentStatusRequest
    {
        public Guid InvoiceItemId { get; set; }
        public int InvoiceItemPaymentStatusId { get; set; }
    }
}
