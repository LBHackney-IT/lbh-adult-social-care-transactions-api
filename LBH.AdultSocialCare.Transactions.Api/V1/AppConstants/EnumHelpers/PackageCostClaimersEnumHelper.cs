using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using System;
using System.Linq;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.EnumHelpers
{
    public static class PackageCostClaimersEnumHelper
    {
        public static bool CheckValidCostClaimer(string packageCostClaimer)
        {
            var costClaimers = Enum.GetValues(typeof(PackageCostClaimersEnum)).Cast<PackageCostClaimersEnum>()
                .Select(c => c.GetDisplayName())
                .ToList();

            return costClaimers.Contains(packageCostClaimer, StringComparer.OrdinalIgnoreCase);
        }
    }
}
