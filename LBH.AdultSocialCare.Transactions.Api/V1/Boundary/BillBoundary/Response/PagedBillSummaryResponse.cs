using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.BillBoundary.Response
{
    public class PagedBillSummaryResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<BillSummaryResponse> Data { get; set; }
    }
}
