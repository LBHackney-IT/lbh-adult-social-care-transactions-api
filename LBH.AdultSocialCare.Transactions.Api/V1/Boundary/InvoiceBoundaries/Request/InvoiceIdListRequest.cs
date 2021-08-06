using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Boundary.InvoiceBoundaries.Request
{
    public class InvoiceIdListRequest
    {
        public IEnumerable<Guid> InvoiceIds { get; set; }
    }
}
