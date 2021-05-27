using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills
{
    public class Bill : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BillId { get; set; }

        public int SupplierId { get; set; }

        public string Ref { get; set; }

        public Guid PackageId { get; set; }

        public DateTimeOffset DateEntered { get; set; }

        public DateTimeOffset DateDue { get; set; }

        public decimal Amount { get; set; }

        public decimal AmountPaid { get; set; }

        public int BillStatusId { get; set; }

        [ForeignKey(nameof(BillStatusId))]
        public BillStatus BillStatus { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }
    }
}
