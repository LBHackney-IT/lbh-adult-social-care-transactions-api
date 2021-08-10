using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response
{
    public class PagedHeldInvoiceResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<HeldInvoiceResponse> Data { get; set; }
    }
}
