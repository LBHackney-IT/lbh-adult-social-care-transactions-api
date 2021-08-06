using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.LedgerDomains
{
    public class LedgerDomain
    {
        public long LedgerId { get; set; }
        public DateTimeOffset DateEntered { get; set; }
        public decimal MoneyIn { get; set; }
        public decimal MoneyOut { get; set; }
        public long PayRunItemId { get; set; }
        public long BillPaymentId { get; set; }
    }
}
