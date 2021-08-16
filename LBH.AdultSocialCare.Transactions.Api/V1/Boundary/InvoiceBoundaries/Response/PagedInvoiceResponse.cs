using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class PagedInvoiceResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<InvoiceResponse> Invoices { get; set; }
    }
}
