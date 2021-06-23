using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities
{
    public class Ledger : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long LedgerId { get; set; }
        public DateTimeOffset DateEntered { get; set; }
        public decimal MoneyIn { get; set; }
        public Guid? InvoicePaymentId { get; set; } // From invoice payments table
        public decimal MoneyOut { get; set; }
        public long? BillPaymentId { get; set; }
        public Guid? PayRunBillId { get; set; } // Supplier bill created from pay run. Recorded as money out.
        [ForeignKey(nameof(PayRunBillId))] public PayRunSupplierBill PayRunSupplierBill { get; set; }
        [ForeignKey(nameof(BillPaymentId))] public BillPayment BillPayment { get; set; }
    }
}
