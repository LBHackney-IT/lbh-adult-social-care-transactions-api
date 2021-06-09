using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Suppliers
{
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the Supplier Name
        /// </summary>
        public string SupplierName { get; set; }
        
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
