using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundaries.Response
{
    public class PagedSupplierMinimalListResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<SupplierMinimalResponse> Data { get; set; }
    }
}
