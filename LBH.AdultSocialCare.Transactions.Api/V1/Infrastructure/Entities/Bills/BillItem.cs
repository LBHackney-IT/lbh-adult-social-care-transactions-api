using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills
{
    public class BillItem : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BillItemId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public Guid BillId { get; set; }

        [ForeignKey((nameof(BillId)))]
        public Bill Bill { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }
    }
}
