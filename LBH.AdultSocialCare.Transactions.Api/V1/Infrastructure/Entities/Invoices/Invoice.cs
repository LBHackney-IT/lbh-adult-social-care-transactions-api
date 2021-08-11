using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class Invoice : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid InvoiceId { get; set; }
        [Required] public string InvoiceNumber { get; set; }
        [Required] public long SupplierId { get; set; }
        [Required] public int PackageTypeId { get; set; }
        [Required] public Guid ServiceUserId { get; set; }
        [Required] public DateTimeOffset DateInvoiced { get; set; }
        [Required] public decimal TotalAmount { get; set; } // Total amount when invoice is being created
        [Required] public float SupplierVATPercent { get; set; }
        [Required] public int InvoiceStatusId { get; set; }
        [Required] public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        public Guid? PackageId { get; set; }
        [Required] public DateTimeOffset DateFrom { get; set; }
        [Required] public DateTimeOffset DateTo { get; set; }

        [ForeignKey(nameof(InvoiceStatusId))] public InvoiceStatus InvoiceStatus { get; set; }
        [ForeignKey(nameof(PackageTypeId))] public PackageType PackageType { get; set; }
        [ForeignKey(nameof(SupplierId))] public Supplier Supplier { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<DisputedInvoiceChat> DisputedInvoiceChat { get; set; }
    }
}
