using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Response
{
    public class InvoiceItemMinimalResponse
    {
        public Guid InvoiceItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public int InvoiceItemPaymentStatusId { get; set; }
        public string ItemName { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public string PriceEffect { get; set; }
        public string ClaimedBy { get; set; }
        public string ReclaimedFrom { get; set; }
        public Guid? SupplierReturnItemId { get; set; } // If the invoice is coming from supplier returns, reference the item here
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
    }
}
