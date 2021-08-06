using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.PayRunBoundaries.Response
{
    public class PagedPayRunSummaryResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<PayRunSummaryResponse> Data { get; set; }
    }
}
