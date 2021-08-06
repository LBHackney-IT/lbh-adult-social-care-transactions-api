using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class PayRunSubTypesSeed : IEntityTypeConfiguration<PayRunSubType>
    {
        public void Configure(EntityTypeBuilder<PayRunSubType> builder)
        {
            builder.HasData(
                new PayRunSubType
                {
                    PayRunSubTypeId = (int) PayRunSubTypeEnum.ResidentialReleaseHolds,
                    SubTypeName = PayRunSubTypeEnum.ResidentialReleaseHolds.ToDescription(),
                    PayRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring
                },
                new PayRunSubType
                {
                    PayRunSubTypeId = (int) PayRunSubTypeEnum.DirectPaymentsReleaseHolds,
                    SubTypeName = PayRunSubTypeEnum.DirectPaymentsReleaseHolds.ToDescription(),
                    PayRunTypeId = (int) PayRunTypeEnum.DirectPayments
                });
        }
    }
}
