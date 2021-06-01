using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.BillsDomain
{
    public class BillFileDomain
    {
        public int BillFileId { get; set; }

        public Guid BillId { get; set; }

        public BillDomain Bill { get; set; }

        public Uri FileUrl { get; set; }

        public string OriginalFileName { get; set; }
    }
}
