using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillItemDomain
    {
        public Guid BillItemId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public Guid BillId { get; set; }

        public BillDomain Bill { get; set; }

        public Guid CreatorId { get; set; }

        public Guid? UpdaterId { get; set; }
    }
}
