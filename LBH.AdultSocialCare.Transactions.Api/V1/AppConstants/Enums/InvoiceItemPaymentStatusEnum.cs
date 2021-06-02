using System.ComponentModel;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum InvoiceItemPaymentStatusEnum
    {
        [Description("Not Started")]
        NotStarted = 1,

        [Description("Held")]
        Held = 2,

        [Description("Paid")]
        Paid = 3,
    }
}
