using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoiceItem : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InvoiceItemId { get; set; }

        public Guid InvoiceId { get; set; }

        public string ItemName { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Vat { get; set; }

        public decimal TotalPrice { get; set; }

        public Guid LedgerId { get; set; }

        [ForeignKey((nameof(InvoiceId)))]
        public Invoice Invoice { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }
    }
}
