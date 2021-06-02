using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceItem : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid InvoiceItemId { get; set; }
        public Guid InvoiceId { get; set; }
        public int InvoiceItemPaymentStatusId { get; set; }
        public string ItemName { get; set; }
        public decimal PricePerUnit { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? SupplierReturnItemId { get; set; } // If the invoice is coming from supplier returns, reference the item here
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }

        [ForeignKey((nameof(InvoiceId)))] public Invoice Invoice { get; set; }
        [ForeignKey((nameof(InvoiceItemPaymentStatusId)))] public InvoiceItemPaymentStatus InvoiceItemPaymentStatus { get; set; }
    }
}
