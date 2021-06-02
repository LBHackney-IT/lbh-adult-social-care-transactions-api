using LBH.AdultSocialCare.Transactions.Api.V1.AppConstants.Enums;
using LBH.AdultSocialCare.Transactions.Api.V1.Extensions;
using LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Entities.PayRunModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.SeedConfiguration
{
    public class PayRunTypesSeed : IEntityTypeConfiguration<PayRunType>
    {
        public void Configure(EntityTypeBuilder<PayRunType> builder)
        {
            builder.HasData(
                new PayRunType
                {
                    PayRunTypeId = (int) PayRunTypeEnum.ResidentialRecurring,
                    TypeName = PayRunTypeEnum.ResidentialRecurring.ToDescription()
                },
                new PayRunType
                {
                    PayRunTypeId = (int) PayRunTypeEnum.DirectPayments,
                    TypeName = PayRunTypeEnum.DirectPayments.ToDescription()
                },
                new PayRunType
                {
                    PayRunTypeId = (int) PayRunTypeEnum.HomeCare,
                    TypeName = PayRunTypeEnum.HomeCare.ToDescription()
                },
                new PayRunType
                {
                    PayRunTypeId = (int) PayRunTypeEnum.ResidentialReleaseHolds,
                    TypeName = PayRunTypeEnum.ResidentialReleaseHolds.ToDescription()
                },
                new PayRunType
                {
                    PayRunTypeId = (int) PayRunTypeEnum.DirectPaymentsReleaseHolds,
                    TypeName = PayRunTypeEnum.DirectPaymentsReleaseHolds.ToDescription()
                });
        }
    }
}
