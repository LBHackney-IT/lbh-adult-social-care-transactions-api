using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains
{
    public class SupplierCreditNotesCreationDomain
    {
        public decimal AmountOverPaid { get; set; }
        public long BillPaymentFromId { get; set; }
        public decimal AmountRemaining { get; set; }
        public long BillPaymentPaidTo { get; set; }
        public DateTimeOffset DatePaidForward { get; set; }
    }
}
