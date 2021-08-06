using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers
{
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the Supplier Name
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the Package Type Id
        /// </summary>
        public int PackageTypeId { get; set; }

        [ForeignKey(nameof(PackageTypeId))] public PackageType Package { get; set; }
    }
}
