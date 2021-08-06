using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains
{
    public class ReleaseHeldInvoiceItemDomain
    {
        public Guid PayRunId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
    }
}
