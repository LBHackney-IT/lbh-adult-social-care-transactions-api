using System.ComponentModel;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum PayRunTypeEnum : int
    {
        [Description("Residential Recurring")]
        ResidentialRecurring = 1,

        [Description("Direct Payments")]
        DirectPayments = 2,

        [Description("Home Care")]
        HomeCare = 3,

        [Description("Residential Release Holds")]
        ResidentialReleaseHolds = 4,

        [Description("Direct Payments Release Holds")]
        DirectPaymentsReleaseHolds = 5
    }
}
