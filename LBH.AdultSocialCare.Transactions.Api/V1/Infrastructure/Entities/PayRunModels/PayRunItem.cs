using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRunItem : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid PayRunItemId { get; set; }
        public Guid PayRunId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        [ForeignKey(nameof(PayRunId))] public PayRun PayRun { get; set; }
        [ForeignKey(nameof(InvoiceId))] public Invoice Invoice { get; set; }
        [ForeignKey(nameof(InvoiceItemId))] public InvoiceItem InvoiceItem { get; set; }
    }
}
