using System.ComponentModel;

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
