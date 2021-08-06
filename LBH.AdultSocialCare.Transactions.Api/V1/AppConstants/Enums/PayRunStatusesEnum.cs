using System.ComponentModel;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum PayRunStatusesEnum : int
    {
        [Description("Draft")]
        Draft = 1,

        [Description("SubmittedForApproval")]
        SubmittedForApproval = 2,

        [Description("Approved")]
        Approved = 3,
    }
}
