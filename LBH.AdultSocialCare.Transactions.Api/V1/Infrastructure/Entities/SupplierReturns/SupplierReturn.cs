using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns
{
    public class SupplierReturn
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SupplierReturnId { get; set; }
        public Guid SuppliersReturnsId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid ServiceUserId { get; set; }
        public Guid PackageId { get; set; }
        public virtual ICollection<SupplierReturnItem> SupplierReturnItems { get; set; }

        [ForeignKey(nameof(SuppliersReturnsId))]
        public SuppliersReturns SuppliersReturns { get; set; }
    }
}
