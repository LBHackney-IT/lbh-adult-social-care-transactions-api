using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices
{
    public class InvoicePayment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid InvoicePaymentId { get; set; }
        public Guid PayRunItemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        [ForeignKey(nameof(PayRunItemId))] public PayRunItem PayRunItem { get; set; }
    }
}
