using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.SupplierBoundary.Response
{
    public class PagedSupplierResponse
    {
        public PagingMetaData PagingMetaData { get; set; }
        public IEnumerable<SupplierResponse> Data { get; set; }
    }
}
