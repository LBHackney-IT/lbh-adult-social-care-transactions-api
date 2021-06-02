using System.ComponentModel;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums
{
    public enum PackageTypeEnum : int
    {
        [Description("Home Care Package")]
        HomeCarePackage = 1,

        [Description("Residential Care Package")]
        ResidentialCarePackage = 2,

        [Description("Day Care Package")]
        DayCarePackage = 3,

        [Description("Nursing Care Package")]
        NursingCarePackage = 4
    }
}
