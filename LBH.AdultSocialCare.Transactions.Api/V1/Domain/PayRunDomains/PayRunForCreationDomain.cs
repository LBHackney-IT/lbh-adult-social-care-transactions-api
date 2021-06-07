using LBH.AdultSocialCare.Transactions.Api.V1.Domain.InvoicesDomains;
using System;
using System.Collections.Generic;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Domain.PayRunDomains
{
    public class PayRunForCreationDomain
    {
        public int PayRunTypeId { get; set; }
        public int? PayRunSubTypeId { get; set; } // Release holds are added as sub types
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? UpdaterId { get; set; }
        public IEnumerable<InvoiceItemMinimalDomain> InvoiceItems { get; set; }
    }
}
