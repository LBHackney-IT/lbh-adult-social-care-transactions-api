using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum InvoiceItemPaymentStatusEnum
    {
        [Description("New")]
        [Display(Name = "New")]
        New = 1,

        [Description("Not Started")]
        [Display(Name = "Not Started")]
        NotStarted = 2,

        [Description("Held")]
        [Display(Name = "Hold")]
        Held = 3,

        [Description("Paid")]
        [Display(Name = "Pay")]
        Paid = 4,

        [Description("Released")]
        [Display(Name = "Release")]
        Released = 5,

        [Description("In New Pay Run")]
        [Display(Name = "In New Pay Run")]
        InNewPayRun = 6
    }
}
