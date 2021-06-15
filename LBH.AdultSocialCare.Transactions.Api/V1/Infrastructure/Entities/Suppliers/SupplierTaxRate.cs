using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers
{
    public class SupplierTaxRate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaxRateId { get; set; }
        public float VATPercentage { get; set; }
        public long SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }
    }
}
