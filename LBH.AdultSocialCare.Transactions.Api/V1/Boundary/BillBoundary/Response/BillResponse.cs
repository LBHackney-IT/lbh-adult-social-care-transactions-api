using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response
{
    public class BillResponse
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
        public int BillPaymentStatusId { get; set; }

        public PackageType PackageType { get; set; }

        public BillStatusResponse BillStatus { get; set; }
        public SupplierResponse Supplier { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        public List<BillItemResponse> BillItems { get; set; }
    }
}
