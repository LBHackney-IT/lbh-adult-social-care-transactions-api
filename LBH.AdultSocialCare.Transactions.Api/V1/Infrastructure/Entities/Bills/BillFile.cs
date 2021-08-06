using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.Bills
{
    public class BillFile : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillFileId { get; set; }

        public long BillId { get; set; }

        [ForeignKey((nameof(BillId)))]
        public Bill Bill { get; set; }

        public Uri FileUrl { get; set; }

        public string OriginalFileName { get; set; }
    }
}
