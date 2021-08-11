using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains
{
    public class InvoiceItemForCreationDomain
    {
        public string ItemName { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? SupplierReturnItemId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
