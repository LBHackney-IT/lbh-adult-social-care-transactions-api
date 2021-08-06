using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillItemCreationDomain
    {
        public long HackneySupplierBillId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public float Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int PackageTypeId { get; set; }
        public float TaxRatePercentage { get; set; }
        public int BillItemStatusId { get; set; }
        public int BillPaymentStatus { get; set; }
        public Guid CreatorId { get; set; }
    }
}
