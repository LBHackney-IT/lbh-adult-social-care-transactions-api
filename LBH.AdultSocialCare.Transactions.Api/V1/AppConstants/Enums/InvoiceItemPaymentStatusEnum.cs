using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum InvoiceItemPaymentStatusEnum
    {
        [Description("Not Started")]
        [Display(Name = "Not Started")]
        NotStarted = 1,

        [Description("Held")]
        [Display(Name = "Hold")]
        Held = 2,

        [Description("Paid")]
        [Display(Name = "Pay")]
        Paid = 3,
    }
}
