using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum InvoiceStatusEnum
    {
        [Description("Draft")]
        [Display(Name = "Draft")]
        Draft = 1,

        [Description("Held")]
        [Display(Name = "Held")]
        Held = 2,

        [Description("Accepted")]
        [Display(Name = "Accepted")]
        Accepted = 3,
    }
}
