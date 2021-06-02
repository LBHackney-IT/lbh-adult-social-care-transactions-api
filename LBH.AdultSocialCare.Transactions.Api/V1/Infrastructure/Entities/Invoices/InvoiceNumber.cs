namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceNumber
    {
        public int InvoiceNumberId { get; set; }
        public string Prefix { get; set; }
        public long CurrentInvoiceNumber { get; set; }
        public string PostFix { get; set; }
    }
}
