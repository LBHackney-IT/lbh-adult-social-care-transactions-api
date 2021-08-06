using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains
{
    public class PayRunInvoicePaymentDomain
    {
        public Guid InvoicePaymentId { get; set; }
        public Guid PayRunItemId { get; set; }
        public Guid PayRunId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public long SupplierId { get; set; }
    }
}
