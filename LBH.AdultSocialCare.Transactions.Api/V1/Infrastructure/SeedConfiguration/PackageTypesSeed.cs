using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants;
using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class PackageTypesSeed : IEntityTypeConfiguration<PackageType>
    {
        public void Configure(EntityTypeBuilder<PackageType> builder)
        {
            var dateTimeOffset = new DateTimeOffset(AppTimeConstants.CreateUpdateDefaultDateTime).ToOffset(TimeSpan.Zero);
            builder.HasData(
                new PackageType
                {
                    DateCreated = dateTimeOffset,
                    DateUpdated = dateTimeOffset,
                    PackageTypeId = (int) PackageTypeEnum.HomeCarePackage,
                    PackageTypeName = PackageTypeEnum.HomeCarePackage.ToDescription()
                },
                new PackageType
                {
                    DateCreated = dateTimeOffset,
                    DateUpdated = dateTimeOffset,
                    PackageTypeId = (int) PackageTypeEnum.ResidentialCarePackage,
                    PackageTypeName = PackageTypeEnum.ResidentialCarePackage.ToDescription()
                },
                new PackageType
                {
                    DateCreated = dateTimeOffset,
                    DateUpdated = dateTimeOffset,
                    PackageTypeId = (int) PackageTypeEnum.DayCarePackage,
                    PackageTypeName = PackageTypeEnum.DayCarePackage.ToDescription()
                },
                new PackageType
                {
                    DateCreated = dateTimeOffset,
                    DateUpdated = dateTimeOffset,
                    PackageTypeId = (int) PackageTypeEnum.NursingCarePackage,
                    PackageTypeName = PackageTypeEnum.NursingCarePackage.ToDescription()
                });
        }
    }
}
