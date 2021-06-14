using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response
{
    public class PayRunDetailsResponse
    {
        public PayRunFlatResponse PayRunDetails { get; set; }
        public PagedInvoiceResponse Invoices { get; set; }
    }
}
