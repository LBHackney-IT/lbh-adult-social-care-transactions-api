using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Response
{
    public class SupplierReturnInsightsResponse
    {
        public Guid SuppliersReturnsId { get; set; }
        public int Suppliers { get; set; }
        public decimal TotalValue { get; set; }
        public int TotalPackages { get; set; }
        public float Returned { get; set; }
        public float InDispute { get; set; }
        public float Accepted { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
