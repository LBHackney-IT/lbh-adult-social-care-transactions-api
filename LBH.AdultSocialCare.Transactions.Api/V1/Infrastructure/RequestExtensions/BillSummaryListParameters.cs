using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.RequestExtensions
{
    public class BillSummaryListParameters : RequestParameters
    {
        public Guid? PackageId { get; set; }
        public int? SupplierId { get; set; }
        public int? BillPaymentStatusId { get; set; }
        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset? ToDate { get; set; }
    }
}
