using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using System.Collections.Generic;
using Common.CustomExceptions;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Gateways.PackageTypeGateways
{
    public class PackageTypeGateway : IPackageTypeGateway
    {
        public bool IsValidPackageType(int packageTypeId)
        {
            var validPackageTypeIds = new List<int>
            {
                (int) PackageTypeEnum.HomeCarePackage,
                (int) PackageTypeEnum.ResidentialCarePackage,
                (int) PackageTypeEnum.DayCarePackage,
                (int) PackageTypeEnum.NursingCarePackage
            };

            if (!validPackageTypeIds.Contains(packageTypeId))
            {
                throw new EntityNotFoundException($"Package type id {packageTypeId} is not valid");
            }

            return true;
        }
    }
}
