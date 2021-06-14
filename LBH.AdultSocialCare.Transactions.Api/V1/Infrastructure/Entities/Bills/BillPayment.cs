using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills
{
    public class BillPayment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BillPaymentId { get; set; }
        public long BillItemId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}
