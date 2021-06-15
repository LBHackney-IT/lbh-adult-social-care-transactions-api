using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains
{
    public class SupplierTaxRateDomain
    {
        public int TaxRateId { get; set; }
        public float VATPercentage { get; set; }
        public long SupplierId { get; set; }

        public SupplierDomain Supplier { get; set; }
    }
}
