using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using System;
using System.Linq;

namespace LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Helpers
{
    public static class EnumHelper
    {
        public static bool IsValidPackageCostClaimer(string packageCostClaimer)
        {
            var costClaimers = Enum.GetValues(typeof(PackageCostClaimersEnum)).Cast<PackageCostClaimersEnum>()
                .Select(c => c.GetDisplayName())
                .ToList();

            return costClaimers.Contains(packageCostClaimer, StringComparer.OrdinalIgnoreCase);
        }

        public static bool IsValidPriceEffect(string invoicePriceEffect)
        {
            var priceEffectsList = Enum.GetValues(typeof(InvoicePriceEffectEnum)).Cast<InvoicePriceEffectEnum>()
                .Select(pi => nameof(pi))
                .ToList();

            return priceEffectsList.Contains(invoicePriceEffect, StringComparer.OrdinalIgnoreCase);
        }
    }
}
