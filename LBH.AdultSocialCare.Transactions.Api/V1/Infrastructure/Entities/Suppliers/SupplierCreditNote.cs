using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers
{
    public class SupplierCreditNote : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CreditNoteId { get; set; }
        public decimal AmountOverPaid { get; set; }
        public long BillPaymentFromId { get; set; }
        public decimal AmountRemaining { get; set; }
        public long BillPaymentPaidTo { get; set; }
        public DateTimeOffset DatePaidForward { get; set; }

        [ForeignKey(nameof(BillPaymentFromId))]
        public BillPayment BillPayment { get; set; }
    }
}
