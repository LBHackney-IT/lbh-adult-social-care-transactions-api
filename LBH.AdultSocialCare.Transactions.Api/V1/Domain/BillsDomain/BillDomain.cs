using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using System;
using System.Collections.Generic;
using LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillDomain
    {
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
        public decimal PaidAmount { get; set; }
        public int? BillPaymentStatusId { get; set; }

        public PackageTypeDomain PackageType { get; set; }

        public BillStatusDomain BillStatus { get; set; }

        public SupplierDomain Supplier { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }

        public IEnumerable<BillItemDomain> BillItem { get; set; }
    }
}
