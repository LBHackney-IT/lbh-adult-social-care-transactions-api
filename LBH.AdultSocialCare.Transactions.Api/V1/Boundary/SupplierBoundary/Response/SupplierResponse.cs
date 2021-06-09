using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response
{
    public class SupplierResponse
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public long SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the Supplier Name
        /// </summary>
        public string SupplierName { get; set; }
    }
}
