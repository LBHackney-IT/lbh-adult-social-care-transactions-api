using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response
{
    public class SupplierTaxRateResponse
    {
        public int TaxRateId { get; set; }
        public float VATPercentage { get; set; }
        public long SupplierId { get; set; }

        public SupplierResponse Supplier { get; set; }
    }
}
