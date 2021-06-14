using LBH.AdultSocialCare.Transactions.Api.V1.Domain.PackageTypeDomains;
using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.SupplierDomains
{
    public class SupplierDomain
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public long SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the Supplier Name
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the Package Type Id
        /// </summary>
        public int PackageTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Package Type Id
        /// </summary>
        public PackageTypeDomain Package { get; set; }

        /// <summary>
        /// Gets or sets the Creator Id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the Updater Id
        /// </summary>
        public Guid? UpdaterId { get; set; }
    }
}
