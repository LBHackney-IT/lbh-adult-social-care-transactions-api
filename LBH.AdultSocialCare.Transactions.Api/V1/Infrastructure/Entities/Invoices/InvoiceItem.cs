using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceItem : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid InvoiceItemId { get; set; }
        [Required] public Guid InvoiceId { get; set; }
        [Required] public string ItemName { get; set; }
        [Required] [Column(TypeName = "decimal(3, 2)")] public decimal PricePerUnit { get; set; }
        [Required] [Column(TypeName = "decimal(3, 2)")] public decimal Quantity { get; set; }
        [Required] [Column(TypeName = "decimal(3, 2)")] public decimal SubTotal { get; set; }
        [Required] [Column(TypeName = "decimal(3, 2)")] public decimal VatAmount { get; set; }
        [Required] [Column(TypeName = "decimal(3, 2)")] public decimal TotalPrice { get; set; }
        public Guid? SupplierReturnItemId { get; set; } // If the invoice is coming from supplier returns, reference the item here
        [Required] public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }

        [ForeignKey((nameof(InvoiceId)))] public Invoice Invoice { get; set; }
        public virtual ICollection<PayRunItem> PayRunItems { get; set; }
    }
}
