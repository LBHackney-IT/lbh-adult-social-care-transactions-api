using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.SupplierReturns
{
    public class SupplierReturnItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemId { get; set; }
        public int SupplierReturnItemStatusId { get; set; }
        public Guid SupplierReturnId { get; set; }
        public string ServiceName { get; set; }
        public float PackageHours { get; set; }
        public float HoursDelivered { get; set; }
        public float PackageVisits { get; set; }
        public float ActualVisits { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal ActualPricePerUnit { get; set; }
        public float Quantity { get; set; }
        public string Comment { get; set; }

        [ForeignKey(nameof(SupplierReturnId))]
        public SupplierReturn SupplierReturn { get; set; }

        [ForeignKey(nameof(SupplierReturnItemStatusId))]
        public SupplierReturnItemStatus SupplierReturnItemStatus { get; set; }
    }
}
