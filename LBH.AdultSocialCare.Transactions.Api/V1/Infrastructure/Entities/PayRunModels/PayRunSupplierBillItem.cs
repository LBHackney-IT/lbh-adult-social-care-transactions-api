using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Invoices;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels
{
    public class PayRunSupplierBillItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid PayRunBillItemId { get; set; }
        public Guid PayRunSupplierBillId { get; set; }
        public Guid PayRunItemId { get; set; }
        public Guid InvoicePaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid? InvoiceItemId { get; set; }
        public decimal PaidAmount { get; set; }
        [ForeignKey(nameof(PayRunSupplierBillId))] public PayRunSupplierBill PayRunSupplierBill { get; set; }
        [ForeignKey(nameof(PayRunItemId))] public PayRunItem PayRunItem { get; set; }
        [ForeignKey(nameof(InvoicePaymentId))] public InvoicePayment InvoicePayment { get; set; }
        [ForeignKey(nameof(InvoiceId))] public Invoice Invoice { get; set; }
        [ForeignKey(nameof(InvoiceItemId))] public InvoiceItem InvoiceItem { get; set; }
    }
}
