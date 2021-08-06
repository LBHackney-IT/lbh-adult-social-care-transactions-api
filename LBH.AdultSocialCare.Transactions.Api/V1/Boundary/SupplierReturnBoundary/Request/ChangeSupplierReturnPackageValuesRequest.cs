using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierReturnBoundary.Request
{
    public class ChangeSupplierReturnPackageValuesRequest
    {
        public Guid SupplierReturnId { get; set; }
        public Guid SupplierReturnItemId { get; set; }
        public float HoursDelivered { get; set; }
        public float ActualVisits { get; set; }
        public string Comment { get; set; }
    }
}
