using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class HeldInvoiceResponse
    {
        public Guid PayRunId { get; set; }
        public DateTimeOffset PayRunDate { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public IEnumerable<InvoiceResponse> Invoices { get; set; }
    }
}
