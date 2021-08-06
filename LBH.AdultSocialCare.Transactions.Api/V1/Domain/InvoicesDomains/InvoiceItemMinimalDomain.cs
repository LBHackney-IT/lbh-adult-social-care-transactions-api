using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class InvoiceItemMinimalDomain
    {
        public Guid InvoiceItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public string ItemName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? SupplierReturnItemId { get; set; } // If the invoice is coming from supplier returns, reference the item here
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
    }
}
