using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum InvoiceStatusEnum
    {
        [Description("New")]
        [Display(Name = "Draft")]
        Draft = 1,

        [Description("Approved")]
        [Display(Name = "Approve")]
        Approved = 2,

        [Description("In Pay Run")]
        [Display(Name = "In pay Run")]
        InPayRun = 3,

        [Description("Held")]
        [Display(Name = "Hold")]
        Held = 4,

        [Description("Accepted")]
        [Display(Name = "Accept")]
        Accepted = 5,

        [Description("Released")]
        [Display(Name = "Release")]
        Released = 6,

        [Description("Paid")]
        [Display(Name = "Paid")]
        Paid = 7,

        [Description("Rejected")]
        [Display(Name = "Reject")]
        Rejected = 8
    }
}
