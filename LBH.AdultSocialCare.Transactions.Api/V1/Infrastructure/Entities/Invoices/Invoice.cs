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
        public string InvoiceNumber { get; set; }
        public long SupplierId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset DateInvoiced { get; set; }
        public decimal TotalAmount { get; set; } // Total amount when invoice is being created
        public float SupplierVATPercent { get; set; }
        public int InvoiceStatusId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }

        [ForeignKey(nameof(InvoiceStatusId))] public InvoiceStatus InvoiceStatus { get; set; }
        [ForeignKey(nameof(PackageTypeId))] public PackageType PackageType { get; set; }
        [ForeignKey(nameof(SupplierId))] public Supplier Supplier { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
