using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum BillStatusEnum : int
    {
        [Description("Outstanding")]
        OutstandingId = 1,

        [Description("Paid")]
        PaidId = 2,

        [Description("Paid Partially")]
        PaidPartiallyId = 3,

        [Description("Overdue")]
        OverdueId = 4
    }
}
