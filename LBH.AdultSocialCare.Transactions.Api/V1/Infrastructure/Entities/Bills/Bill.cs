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
        public long BillId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid PackageId { get; set; }
        public string SupplierRef { get; set; }
        public long SupplierId { get; set; }
        public DateTimeOffset ServiceFromDate { get; set; }
        public DateTimeOffset ServiceToDate { get; set; }
        public DateTimeOffset DateBilled { get; set; }
        public DateTimeOffset BillDueDate { get; set; }
        public decimal TotalBilled { get; set; }
        public int BillPaymentStatusId { get; set; }

        [ForeignKey(nameof(PackageTypeId))]
        public PackageType PackageType { get; set; }

        [ForeignKey(nameof(BillPaymentStatusId))]
        public BillStatus BillStatus { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
