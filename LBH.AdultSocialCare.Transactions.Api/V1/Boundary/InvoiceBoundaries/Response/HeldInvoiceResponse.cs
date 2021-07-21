using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class HeldInvoiceResponse
    {
        public Guid PayRunId { get; set; }
        public Guid PayRunItemId { get; set; }
        public DateTimeOffset PayRunDate { get; set; }
        public IEnumerable<InvoiceResponse> Invoices { get; set; }
    }
}
