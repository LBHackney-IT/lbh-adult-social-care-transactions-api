using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities
{
    public class Ledger : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LedgerId { get; set; }
        public DateTimeOffset DateEntered { get; set; }
        public decimal MoneyIn { get; set; }
        public long PayRunItemId { get; set; }
        public long BillPaymentId { get; set; }
    }
}
