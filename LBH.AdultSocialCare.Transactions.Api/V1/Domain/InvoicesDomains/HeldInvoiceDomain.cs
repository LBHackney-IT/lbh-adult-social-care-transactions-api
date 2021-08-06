using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class HeldInvoiceDomain
    {
        public Guid PayRunId { get; set; }
        public DateTimeOffset PayRunDate { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public IEnumerable<InvoiceDomain> Invoices { get; set; }
    }
}
