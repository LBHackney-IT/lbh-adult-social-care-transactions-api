using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains
{
    public class SupplierReturnItemDomain
    {
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

        public SupplierReturnDomain SupplierReturn { get; set; }

        public SupplierReturnItemStatusDomain SupplierReturnItemStatus { get; set; }
    }
}
