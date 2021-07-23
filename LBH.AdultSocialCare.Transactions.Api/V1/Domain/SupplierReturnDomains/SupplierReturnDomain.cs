using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierReturnDomains
{
    public class SupplierReturnDomain
    {
        public Guid SupplierReturnId { get; set; }
        public Guid SuppliersReturnsId { get; set; }
        public int PackageTypeId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid ServiceUserId { get; set; }
        public Guid PackageId { get; set; }

        public IEnumerable<SupplierReturnItemDomain> SupplierReturnItem { get; set; }
    }
}
