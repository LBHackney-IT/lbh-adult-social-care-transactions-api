using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillPaymentDomain
    {
        public long BillPaymentId { get; set; }
        public long BillItemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
