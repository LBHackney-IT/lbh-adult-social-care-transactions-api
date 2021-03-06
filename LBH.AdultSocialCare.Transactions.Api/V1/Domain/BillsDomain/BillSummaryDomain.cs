using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillSummaryDomain
    {
        public long BillId { get; set; }
        public Guid PackageId { get; set; }
        public string SupplierRef { get; set; }
        public string SupplierName { get; set; }
        public DateTimeOffset ServiceFromDate { get; set; }
        public DateTimeOffset ServiceToDate { get; set; }
        public DateTimeOffset DateBilled { get; set; }
        public DateTimeOffset BillDueDate { get; set; }
        public decimal TotalBilled { get; set; }
        public decimal PaidAmount { get; set; }
        public string StatusName { get; set; }
    }
}
